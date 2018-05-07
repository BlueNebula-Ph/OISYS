namespace Oisys.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper;
    using BlueNebula.Common.Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Oisys.Web.DTO;
    using Oisys.Web.Filters;
    using Oisys.Web.Models;
    using Oisys.Web.Helpers;

    /// <summary>
    /// <see cref="InvoiceController"/> class handles Invoice basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ValidateModel]
    public class InvoiceController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<Invoice, InvoiceSummary> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        public InvoiceController(
            OisysDbContext context,
            IMapper mapper,
            ISummaryListBuilder<Invoice, InvoiceSummary> builder)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
        }

        /// <summary>
        /// Returns list of active <see cref="Invoice"/>
        /// </summary>
        /// <param name="filter"><see cref="InvoiceFilterRequest"/></param>
        /// <returns>List of Invoice</returns>
        [HttpPost("search", Name = "GetAllInvoices")]
        public async Task<IActionResult> GetAll([FromBody] InvoiceFilterRequest filter)
        {
            // get list of active invoices (not deleted)
            var list = this.context.Invoices
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (!string.IsNullOrEmpty(filter?.SearchTerm))
            {
                list = list.Where(c => c.Code.Contains(filter.SearchTerm));
            }

            if (filter?.DateFrom != null || filter?.DateTo != null)
            {
                filter.DateFrom = filter.DateFrom == null || filter.DateFrom == DateTime.MinValue ? DateTime.Today : filter.DateFrom;
                filter.DateTo = filter.DateTo == null || filter.DateTo == DateTime.MinValue ? DateTime.Today : filter.DateTo;
                list = list.Where(c => c.Date >= filter.DateFrom && c.Date < filter.DateTo.Value.AddDays(1));
            }

            // sort
            var ordering = $"Code {Constants.DefaultSortDirection}";
            if (!string.IsNullOrEmpty(filter?.SortBy))
            {
                ordering = $"{filter.SortBy} {filter.SortDirection}";
            }

            list = list.OrderBy(ordering);

            var entities = await this.builder.BuildAsync(list, filter);

            return this.Ok(entities);
        }

        /// <summary>
        /// Gets a specific <see cref="Invoice"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Invoice</returns>
        [HttpGet("{id}", Name = "GetInvoice")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.Invoices
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<InvoiceSummary>(entity);

            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="Invoice"/>.
        /// </summary>
        /// <param name="entity">entity to be created</param>
        /// <returns>Invoice</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveInvoiceRequest entity)
        {
            var invoice = this.mapper.Map<Invoice>(entity);
            await this.context.Invoices.AddAsync(invoice);
            await this.context.SaveChangesAsync();

            return this.CreatedAtRoute("GetInvoice", new { id = invoice.Id }, entity);
        }

        /// <summary>
        /// Updates a specific <see cref="Invoice"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] SaveInvoiceRequest entity)
        {
            var invoice = await this.context.Invoices.SingleOrDefaultAsync(t => t.Id == id);
            if (invoice == null)
            {
                return this.NotFound(id);
            }

            try
            {
                this.mapper.Map(entity, invoice);
                this.context.Update(invoice);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="Invoice"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var invoice = await this.context.Invoices.SingleOrDefaultAsync(t => t.Id == id);
            if (invoice == null)
            {
                return this.NotFound(id);
            }

            invoice.IsDeleted = true;
            this.context.Update(invoice);
            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}
