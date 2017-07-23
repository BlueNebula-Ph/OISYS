namespace Oisys.Service.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Oisys.Service.DTO;

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
        /// <returns>List of Customer</returns>
        [HttpGet(Name = "GetAllCustomers")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await this.context.Customers.Where(c => !c.IsDeleted).ToListAsync();
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
        public async Task<IActionResult> Create([FromBody] Customer entity)
        {
            if (entity == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await this.context.Customers.AddAsync(entity);
            await this.context.SaveChangesAsync();

            return this.CreatedAtRoute("GetCustomer", new { id = entity.Id }, entity);
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