namespace Oisys.Service.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly OisysDbContext context;

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
                    Term = "term1",
                    Discount = "discount",
                    PriceList = "a",
                });
                this.context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {
            return this.context.Customers.ToList();
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetById(long id)
        {
            var item = this.context.Customers.FirstOrDefault(c => c.Id == id);
            if (item == null)
            {
                return this.NotFound();
            }

            return new ObjectResult(item);
        }
    }
}