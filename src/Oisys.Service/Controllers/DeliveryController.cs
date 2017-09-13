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
    using Oisys.Service.Services.Interfaces;

    /// <summary>
    /// <see cref="DeliveryController"/> class handles Delivery basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DeliveryController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<Delivery, DeliverySummary> builder;
        private readonly IAdjustmentService adjustmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        /// <param name="adjustmentService">Adjustment Service</param>
        public DeliveryController(OisysDbContext context, IMapper mapper, ISummaryListBuilder<Delivery, DeliverySummary> builder, IAdjustmentService adjustmentService)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
            this.adjustmentService = adjustmentService;
        }

        /// <summary>
        /// Returns list of active <see cref="Delivery"/>
        /// </summary>
        /// <param name="filter"><see cref="DeliveryFilterRequest"/></param>
        /// <returns>List of Deliverys</returns>
        [HttpPost("search", Name = "GetAllDelivery")]
        public async Task<IActionResult> GetAll([FromBody]DeliveryFilterRequest filter)
        {
            // get list of active customers (not deleted)
            var list = this.context.Deliveries
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (!string.IsNullOrEmpty(filter?.SearchTerm))
            {
                list = list.Where(c => c.Code.Contains(filter.SearchTerm));
            }

            if (!(filter?.CustomerId).IsNullOrZero())
            {
                list = list.Where(c => c.Details.Any(a => a.Order.CustomerId == filter.CustomerId));
            }

            if (filter?.DateFrom != null)
            {
                list = list.Where(c => c.Date >= filter.DateFrom && c.Date <= filter.DateFrom);
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
        /// Gets a specific <see cref="Delivery"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Delivery</returns>
        [HttpGet("{id}", Name = "GetDelivery")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.Deliveries
                .AsNoTracking()
                .Include(c => c.Details)
                .Include("Details.Order")
                .Include("Details.Order.Customer")
                .Include("Details.Item")
                .SingleOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<DeliverySummary>(entity);

            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="Delivery"/>.
        /// </summary>
        /// <param name="entity">entity to be created</param>
        /// <returns>Delivery</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SaveDeliveryRequest entity)
        {
            if (entity == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var order = this.mapper.Map<Delivery>(entity);

            foreach (var detail in order.Details)
            {
                var item = this.context.Items.AsNoTracking().SingleOrDefault(c => c.Id == detail.ItemId);
                this.adjustmentService.ModifyCurrentQuantity(item, detail.Quantity, AdjustmentType.Deduct);
            }

            await this.context.Deliveries.AddAsync(order);

            await this.context.SaveChangesAsync();

            var mappedDelivery = this.mapper.Map<DeliverySummary>(order);

            return this.CreatedAtRoute("GetDelivery", new { id = order.Id }, mappedDelivery);
        }

        /// <summary>
        /// Updates a specific <see cref="Delivery"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody]SaveDeliveryRequest entity)
        {
            if (entity == null || entity.Id == 0 || id == 0)
            {
                return this.BadRequest();
            }

            var order = await this.context.Deliveries
                .AsNoTracking()
                .Include(c => c.Details)
                .Include("Details.Item")
                .SingleOrDefaultAsync(t => t.Id == id);

            if (order == null)
            {
                return this.NotFound(id);
            }

            try
            {
                foreach (var newDetail in entity.Details)
                {
                    var oldDetail = order.Details.SingleOrDefault(c => c.Id == newDetail.Id);

                    if (oldDetail != null)
                    {
                        this.adjustmentService.ModifyCurrentQuantity(oldDetail.Item, oldDetail.Quantity, AdjustmentType.Add);

                        if (newDetail.IsDeleted)
                        {
                            this.context.Remove(oldDetail);
                        }
                        else
                        {
                            this.adjustmentService.ModifyCurrentQuantity(oldDetail.Item, newDetail.Quantity, AdjustmentType.Deduct);
                        }
                    }
                }

                this.mapper.Map(entity, order);

                this.context.Update(order);

                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="Delivery"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var order = await this.context.Deliveries
                .Include(c => c.Details)
                .Include("Details.Item")
                .SingleOrDefaultAsync(c => c.Id == id);

            if (order == null)
            {
                return this.NotFound(id);
            }

            order.IsDeleted = true;

            foreach (var detail in order.Details)
            {
                this.adjustmentService.ModifyCurrentQuantity(detail.Item, detail.Quantity, AdjustmentType.Add);
            }

            this.context.RemoveRange(order.Details);

            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}
