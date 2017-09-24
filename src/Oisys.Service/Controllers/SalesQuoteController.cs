namespace Oisys.Service.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Oisys.Service.DTO;
    using Oisys.Service.Helpers;
    using Oisys.Service.Models;
    using Oisys.Service.Services.Interfaces;

    /// <summary>
    /// <see cref="SalesQuoteController"/> class handles SalesQuote basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/SalesQuote")]
    public class SalesQuoteController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<SalesQuote, SalesQuoteSummary> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesQuoteController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        public SalesQuoteController(OisysDbContext context, IMapper mapper, ISummaryListBuilder<SalesQuote, SalesQuoteSummary> builder)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
        }

        /// <summary>
        /// Returns list of active <see cref="SalesQuote"/>
        /// </summary>
        /// <param name="filter"><see cref="SalesQuoteFilterRequest"/></param>
        /// <returns>List of SalesQuote</returns>
        [HttpPost("search", Name = "GetAllSalesQuote")]
        public async Task<IActionResult> GetAll([FromBody]SalesQuoteFilterRequest filter)
        {
            // get list of active sales quote (not deleted)
            var list = this.context.SalesQuotes
                .Include(c => c.Customer)
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (!string.IsNullOrEmpty(filter?.SearchTerm))
            {
                list = list.Where(c => c.Code.Contains(filter.SearchTerm));
            }

            if (!(filter?.ProvinceId).IsNullOrZero())
            {
                list = list.Where(c => c.Customer.ProvinceId == filter.ProvinceId);
            }

            if (!(filter?.CustomerId).IsNullOrZero())
            {
                list = list.Where(c => c.CustomerId == filter.CustomerId);
            }

            if (filter?.DateFrom != null || filter?.DateTo != null)
            {
                filter.DateFrom = filter.DateFrom == null || filter.DateFrom == DateTime.MinValue ? DateTime.Today : filter.DateFrom;
                filter.DateTo = filter.DateTo == null || filter.DateTo == DateTime.MinValue ? DateTime.Today : filter.DateTo;
                list = list.Where(c => c.Date >= filter.DateFrom && c.Date < filter.DateTo.Value.AddDays(1));
            }

            if (!(filter?.ItemId).IsNullOrZero())
            {
                list = list.Where(c => c.Details.Any(d => d.ItemId == filter.ItemId));
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
        /// Gets a specific <see cref="SalesQuote"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>SalesQuote</returns>
        [HttpGet("{id}", Name = "GetSalesQuote")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.SalesQuotes
                .AsNoTracking()
                .Include(c => c.Customer)
                .Include(c => c.Details)
                    .ThenInclude(d => d.Item)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<SalesQuoteSummary>(entity);

            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="SalesQuote"/>.
        /// </summary>
        /// <param name="entity">entity to be created</param>
        /// <returns>SalesQuote</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SaveSalesQuoteRequest entity)
        {
            if (entity == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var salesQuote = this.mapper.Map<SalesQuote>(entity);

            await this.context.SalesQuotes.AddAsync(salesQuote);

            await this.context.SaveChangesAsync();

            var mappedSalesQuote = this.mapper.Map<SalesQuoteSummary>(salesQuote);

            return this.CreatedAtRoute("GetSalesQuote", new { id = salesQuote.Id }, mappedSalesQuote);
        }

        /// <summary>
        /// Updates a specific <see cref="SalesQuote"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody]SaveSalesQuoteRequest entity)
        {
            if (entity == null || entity.Id == 0 || id == 0 || id != entity.Id)
            {
                return this.BadRequest();
            }

            var salesQuote = await this.context.SalesQuotes.SingleOrDefaultAsync(t => t.Id == id);
            if (salesQuote == null)
            {
                return this.NotFound(id);
            }

            try
            {
                this.mapper.Map(entity, salesQuote);
                this.context.Update(salesQuote);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="SalesQuote"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var salesQuote = await this.context.SalesQuotes
                .Include(c => c.Details)
                .Include("Details.Item")
                .SingleOrDefaultAsync(c => c.Id == id);

            if (salesQuote == null)
            {
                return this.NotFound(id);
            }

            salesQuote.IsDeleted = true;

            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}