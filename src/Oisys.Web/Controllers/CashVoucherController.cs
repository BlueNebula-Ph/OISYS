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
    using Oisys.Web.Helpers;
    using Oisys.Web.Models;

    /// <summary>
    /// <see cref="CashVoucherController"/> class handles CashVoucher basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ValidateModel]
    public class CashVoucherController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<CashVoucher, CashVoucherSummary> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="CashVoucherController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        public CashVoucherController(
            OisysDbContext context,
            IMapper mapper,
            ISummaryListBuilder<CashVoucher, CashVoucherSummary> builder)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
        }

        /// <summary>
        /// Returns list of active <see cref="CashVoucher"/>
        /// </summary>
        /// <param name="filter"><see cref="CashVoucherFilterRequest"/></param>
        /// <returns>List of CashVoucher</returns>
        [HttpPost("search", Name = "GetAllCashVouchers")]
        public async Task<IActionResult> GetAll([FromBody] CashVoucherFilterRequest filter)
        {
            // get list of active cashVouchers (not deleted)
            var list = this.context.CashVouchers
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (!string.IsNullOrEmpty(filter?.SearchTerm))
            {
                list = list.Where(c => c.VoucherNumber.Contains(filter.SearchTerm));
            }

            if (filter?.DateFrom != null || filter?.DateTo != null)
            {
                filter.DateFrom = filter.DateFrom == null || filter.DateFrom == DateTime.MinValue ? DateTime.Today : filter.DateFrom;
                filter.DateTo = filter.DateTo == null || filter.DateTo == DateTime.MinValue ? DateTime.Today : filter.DateTo;
                list = list.Where(c => c.Date >= filter.DateFrom && c.Date < filter.DateTo.Value.AddDays(1));
            }

            // sort
            var ordering = $"VoucherNumber {Constants.DefaultSortDirection}";
            if (!string.IsNullOrEmpty(filter?.SortBy))
            {
                ordering = $"{filter.SortBy} {filter.SortDirection}";
            }

            list = list.OrderBy(ordering);

            var entities = await this.builder.BuildAsync(list, filter);

            return this.Ok(entities);
        }

        /// <summary>
        /// Gets a specific <see cref="CashVoucher"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>CashVoucher</returns>
        [HttpGet("{id}", Name = "GetCashVoucher")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.CashVouchers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<CashVoucherSummary>(entity);

            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="CashVoucher"/>.
        /// </summary>
        /// <param name="entity">entity to be created</param>
        /// <returns>CashVoucher</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveCashVoucherRequest entity)
        {
            var cashVoucher = this.mapper.Map<CashVoucher>(entity);
            await this.context.CashVouchers.AddAsync(cashVoucher);
            await this.context.SaveChangesAsync();

            return this.CreatedAtRoute("GetCashVoucher", new { id = cashVoucher.Id }, entity);
        }

        /// <summary>
        /// Updates a specific <see cref="CashVoucher"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] SaveCashVoucherRequest entity)
        {
            var cashVoucher = await this.context.CashVouchers.SingleOrDefaultAsync(t => t.Id == id);
            if (cashVoucher == null)
            {
                return this.NotFound(id);
            }

            try
            {
                this.mapper.Map(entity, cashVoucher);
                this.context.Update(cashVoucher);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="CashVoucher"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var cashVoucher = await this.context.CashVouchers.SingleOrDefaultAsync(t => t.Id == id);
            if (cashVoucher == null)
            {
                return this.NotFound(id);
            }

            cashVoucher.IsDeleted = true;
            this.context.Update(cashVoucher);
            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}