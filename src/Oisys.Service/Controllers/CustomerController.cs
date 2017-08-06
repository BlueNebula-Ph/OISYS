namespace Oisys.Service.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Oisys.Service.DTO;
    using Oisys.Service.Helpers;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// <see cref="CustomerController"/> class handles Customer basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// /// <param name="mapper">Automapper</param>
        public CustomerController(OisysDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
            var list = this.context.Customers.Where(c => !c.IsDeleted);

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

            // TODO: Should be returning ViewModel
            var customers = await SummaryList<CustomerSummary>.CreateAsync(this.mapper.Map<IQueryable<CustomerSummary>>(list), pageNumber, pageSize);

            return this.Ok(customers);
        }

        /// <summary>
        /// Gets a specific <see cref="Customer"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Customer</returns>
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> GetById(long id)
        {
            var customer = await this.context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
            {
                return this.NotFound(id);
            }

            return this.Ok(customer);
        }

        /// <summary>
        /// Creates a <see cref="Customer"/>.
        /// </summary>
        /// <param name="entity">entity</param>
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

            return this.CreatedAtRoute("GetCustomer", new { id = customer.Id }, this.mapper.Map<CustomerSummary>(entity));
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
            var customer = await this.context.Customers.FirstAsync(t => t.Id == id);
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
        [HttpPost("{id}/transaction")]
        public async Task<IActionResult> AddCustomerTransaction(long customerId, [FromBody] SaveCustomerTrxRequest entity)
        {
            if (entity == null || customerId == 0 || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var transaction = this.mapper.Map<CustomerTransaction>(entity);

            await this.context.CustomerTransactions.AddAsync(transaction);
            await this.context.SaveChangesAsync();

            var customer = this.context.Customers.FirstOrDefault(c => c.Id == customerId);

            return this.CreatedAtRoute("GetCustomer", new { id = customerId }, this.mapper.Map<CustomerSummary>(customer));
        }
    }
}