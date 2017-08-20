namespace Oisys.Service.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Oisys.Service.DTO;
    using Oisys.Service.Helpers;

    /// <summary>
    /// <see cref="CustomerController"/> class handles Customer basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<Customer, CustomerSummary> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        public CustomerController(OisysDbContext context, IMapper mapper, ISummaryListBuilder<Customer, CustomerSummary> builder)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
        }

        /// <summary>
        /// Returns list of active <see cref="Customer"/>
        /// </summary>
        /// <param name="filter"><see cref="CustomerFilterRequest"/></param>
        /// <returns>List of Customer</returns>
        [HttpPost("search", Name = "GetAllCustomers")]
        public async Task<IActionResult> GetAll([FromBody]CustomerFilterRequest filter)
        {
            // get list of active customers (not deleted)
            var list = this.context.Customers
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (!string.IsNullOrEmpty(filter?.SearchTerm))
            {
                list = list.Where(c => c.Tag.Contains(filter.SearchTerm));
            }

            if (!(filter?.ProvinceId).IsNullOrZero())
            {
                list = list.Where(c => c.ProvinceId == filter.ProvinceId);
            }

            if (!(filter?.CityId).IsNullOrZero())
            {
                list = list.Where(c => c.CityId == filter.CityId);
            }

            // sort
            var ordering = $"Name {Constants.DefaultSortDirection}";
            if (!string.IsNullOrEmpty(filter?.SortBy))
            {
                ordering = $"{filter.SortBy} {filter.SortDirection}";
            }

            list = list.OrderBy(ordering);

            // paging
            var pageNumber = (filter?.PageIndex).IsNullOrZero() ? Constants.DefaultPageIndex : filter.PageIndex;
            var pageSize = (filter?.PageSize).IsNullOrZero() ? Constants.DefaultPageSize : filter.PageSize;

            var entities = await this.builder.BuildAsync(list, pageNumber, pageSize);

            return this.Ok(entities);
        }

        /// <summary>
        /// Returns list of active <see cref="Customer"/>
        /// </summary>
        /// <returns>List of Customers</returns>
        [HttpGet("lookup", Name = "GetCustomerLookup")]
        public IActionResult GetLookup()
        {
            // get list of active items (not deleted)
            var list = this.context.Customers
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // sort
            var ordering = $"Name {Constants.DefaultSortDirection}";

            list = list.OrderBy(ordering);

            var entities = list.ProjectTo<CustomerLookup>();

            return this.Ok(entities);
        }

        /// <summary>
        /// Gets a specific <see cref="Customer"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Customer</returns>
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.Customers
                .AsNoTracking()
                .Include(c => c.City)
                .Include(c => c.Province)
                .Include(c => c.Transactions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<CustomerSummary>(entity);

            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="Customer"/>.
        /// </summary>
        /// <param name="entity">entity to be created</param>
        /// <returns>Customer</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveCustomerRequest entity)
        {
            if (entity == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var customer = this.mapper.Map<Customer>(entity);
            await this.context.Customers.AddAsync(customer);
            await this.context.SaveChangesAsync();

            var mappedCustomer = this.mapper.Map<CustomerSummary>(entity);

            return this.CreatedAtRoute("GetCustomer", new { id = customer.Id }, mappedCustomer);
        }

        /// <summary>
        /// Updates a specific <see cref="Customer"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] SaveCustomerRequest entity)
        {
            if (entity == null || entity.Id == 0 || id == 0)
            {
                return this.BadRequest();
            }

            var customer = await this.context.Customers.SingleOrDefaultAsync(t => t.Id == id);
            if (customer == null)
            {
                return this.NotFound(id);
            }

            try
            {
                this.mapper.Map(entity, customer);
                this.context.Update(customer);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="Customer"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var customer = await this.context.Customers.SingleOrDefaultAsync(t => t.Id == id);
            if (customer == null)
            {
                return this.NotFound(id);
            }

            customer.IsDeleted = true;
            this.context.Update(customer);
            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }

        /// <summary>
        /// Adds a transaction record to a <see cref="Customer"/> record
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="entity">Entity to add</param>
        /// <returns><see cref="Customer"/></returns>
        [HttpPost("{customerId}/transaction")]
        public async Task<IActionResult> AddCustomerTransaction(long customerId, [FromBody] SaveCustomerTrxRequest entity)
        {
            if (entity == null || customerId == 0 || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var transaction = this.mapper.Map<CustomerTransaction>(entity);

            await this.context.CustomerTransactions.AddAsync(transaction);
            await this.context.SaveChangesAsync();

            return this.Ok(entity);
        }

        /// <summary>
        /// Gets all transactions of a customer
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="filter">Filter values</param>
        /// <returns>Returns list of <see cref="CustomerTransactionSummary"/></returns>
        [HttpPost("{customerId}/gettransactions")]
        public async Task<IActionResult> GetCustomerTransactions(long customerId, [FromBody] CustomerTransactionFilterRequest filter)
        {
            if (customerId == 0 || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var customer = await this.context.Customers
                .Include(c => c.Transactions)
                .SingleOrDefaultAsync(c => c.Id == customerId);

            if (filter.DateFrom == null || filter.DateFrom == DateTime.MinValue)
            {
                filter.DateFrom = DateTime.Now;
            }

            if (filter.DateTo == null || filter.DateTo == DateTime.MinValue)
            {
                filter.DateTo = DateTime.Now;
            }

            var transactions = customer.Transactions.Where(c => c.Date >= filter.DateFrom && c.Date <= filter.DateTo);

            var mappedTransactions = transactions.AsQueryable().ProjectTo<CustomerTransactionSummary>();

            return this.Ok(mappedTransactions);
        }
    }
}