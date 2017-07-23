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
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

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

            if (this.context.Customers.Count() == 0)
            {
                this.context.Customers.Add(new Customer
                {
                    Code = "A",
                    Name = "A",
                    Email = "a@a.com",
                    ContactNumber = "a",
                    ContactPerson = "a",
                    Address = "A",
                    CityId = 1,
                    ProvinceId = 1,
                    Terms = "term1",
                    Discount = "discount",
                    PriceList = "a",
                });
                this.context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns list of active <see cref="Customer"/>
        /// </summary>
        /// <param name="data"><see cref="CustomerFilterRequest"/></param>
        /// <returns>List of Customer</returns>
        [HttpGet(Name = "GetAllCustomers")]
        public async Task<IActionResult> GetAll(string data)
        {
            CustomerFilterRequest filter = null;

            if (!string.IsNullOrEmpty(data))
            {
                try
                {
                    string o = JsonConvert.DeserializeObject(data).ToString();
                    var v = JObject.Parse(o);
                    filter = v.ToObject<CustomerFilterRequest>();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            // get list of active customers (not deleted)
            var list = this.context.Customers.Where(c => !c.IsDeleted);

            // filter

            // sort
            if (filter == null)
            {
                list = list.OrderBy(c => c.Name);
            }
            else
            {
                list = list.OrderBy(filter.SortBy, filter.SortDirection, null);
            }

            // paging
            var customers = await SummaryList<Customer>.CreateAsync(list, filter == null ? Constants.DefaultPageIndex : filter.PageIndex, filter == null ? Constants.DefaultPageSize : filter.PageSize);

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

            return this.CreatedAtRoute("GetCustomer", new { id = customer.Id }, entity);
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
    }
}