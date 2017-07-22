namespace Oisys.Service.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly OisysDbContext context;

        // Constructor
        public CustomerController(OisysDbContext context)
        {
            this.context = context;

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

        // Get all customers
        [HttpGet(Name = "GetAllCustomers")]
        public IActionResult GetAll()
        {
            return this.Ok(this.context.Customers.Where(c => !c.IsDeleted).ToList());
        }

        // Get customer by ID
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetById(long id)
        {
            var item = this.context.Customers.FirstOrDefault(c => c.Id == id);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Ok(item);
        }

        // Create a new customer
        [HttpPost]
        public IActionResult Create([FromBody] Customer entity)
        {
            if (entity == null)
            {
                return this.BadRequest();
            }

            this.context.Customers.Add(entity);
            this.context.SaveChanges();

            return this.CreatedAtRoute("GetCustomer", new { id = entity.Id }, entity);
        }

        // Update a customer
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Customer entity)
        {
            if (entity == null || entity.Id == 0 || id == 0)
            {
                return this.BadRequest();
            }

            var customer = this.context.Customers.SingleOrDefault(t => t.Id == id);
            if (customer == null)
            {
                return this.NotFound();
            }

            // TODO: Use Automapper
            customer.Address = entity.Address;
            customer.CityId = entity.CityId;
            customer.Code = entity.Code;
            customer.ContactNumber = entity.ContactNumber;
            customer.ContactPerson = entity.ContactPerson;
            customer.Discount = entity.Discount;
            customer.Email = entity.Email;
            customer.LastUpdatedDate = DateTime.Now;
            customer.LastUpdatedBy = 1;
            customer.Name = entity.Name;
            customer.PriceList = entity.PriceList;
            customer.Terms = entity.Terms;

            this.context.Update(customer);
            this.context.SaveChanges();

            return this.CreatedAtRoute("GetCustomer", new { id = entity.Id }, entity);
        }

        // Delete a customer
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var customer = this.context.Customers.First(t => t.Id == id);
            if (customer == null)
            {
                return this.NotFound();
            }

            customer.IsDeleted = true;
            this.context.Update(customer);
            this.context.SaveChanges();

            return new NoContentResult();
        }
    }
}