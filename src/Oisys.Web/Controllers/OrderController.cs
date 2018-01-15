namespace Oisys.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Oisys.Web.DTO;
    using Oisys.Web.Filters;
    using Oisys.Web.Helpers;
    using Oisys.Web.Services.Interfaces;

    /// <summary>
    /// <see cref="OrderController"/> class handles Order basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ValidateModel]
    public class OrderController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<Order, OrderSummary> builder;
        private readonly IAdjustmentService adjustmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        /// <param name="adjustmentService">Adjustment Service</param>
        public OrderController(OisysDbContext context, IMapper mapper, ISummaryListBuilder<Order, OrderSummary> builder, IAdjustmentService adjustmentService)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
            this.adjustmentService = adjustmentService;
        }

        /// <summary>
        /// Returns list of active <see cref="Order"/>
        /// </summary>
        /// <param name="filter"><see cref="OrderFilterRequest"/></param>
        /// <returns>List of Orders</returns>
        [HttpPost("search", Name = "GetAllOrders")]
        public async Task<IActionResult> GetAll([FromBody]OrderFilterRequest filter)
        {
            // get list of active orders (not deleted)
            var list = this.context.Orders
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
        /// Gets a specific <see cref="Order"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Order</returns>
        [HttpGet("{id}", Name = "GetOrder")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.Orders
                .AsNoTracking()
                .Include(c => c.Customer)
                .Include(c => c.Details)
                    .ThenInclude(d => d.Item)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<OrderSummary>(entity);

            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="Order"/>.
        /// </summary>
        /// <param name="entity">entity to be created</param>
        /// <returns>Order</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SaveOrderRequest entity)
        {
            var order = this.mapper.Map<Order>(entity);

            foreach (var detail in order.Details)
            {
                var item = await this.context.Items.FindAsync(detail.ItemId);
                this.adjustmentService.ModifyQuantity(QuantityType.CurrentQuantity, item, detail.Quantity, AdjustmentType.Deduct, Constants.AdjustmentRemarks.OrderCreated);
            }

            await this.context.Orders.AddAsync(order);

            await this.context.SaveChangesAsync();

            var mappedOrder = this.mapper.Map<OrderSummary>(order);

            return this.CreatedAtRoute("GetOrder", new { id = order.Id }, mappedOrder);
        }

        /// <summary>
        /// Updates a specific <see cref="Order"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody]SaveOrderRequest entity)
        {
            var orderExists = await this.context.Orders
                .AnyAsync(c => c.Id == id);

            if (!orderExists)
            {
                return this.NotFound(id);
            }

            try
            {
                // Set the state to modified
                entity.State = ObjectState.Modified;

                // Adjust current quantities accordingly
                foreach (var updatedOrderDetail in entity.Details)
                {
                    var orderDetailItem = await this.context.Items.FindAsync(updatedOrderDetail.ItemId);
                    if (orderDetailItem != null)
                    {
                        // Fetch the old detail as no tracking to not interfere with the context-tracked order detail
                        var oldDetail = await this.context.OrderDetails
                            .AsNoTracking()
                            .SingleOrDefaultAsync(c => c.Id == updatedOrderDetail.Id);

                        // If the order detail exists, return the old quantities back
                        if (oldDetail != null)
                        {
                            updatedOrderDetail.State = ObjectState.Modified;
                            this.adjustmentService.ModifyQuantity(QuantityType.CurrentQuantity, orderDetailItem, oldDetail.Quantity, AdjustmentType.Add, Constants.AdjustmentRemarks.OrderUpdated);

                            // If a delete is issued to the order detail, remove that order detail
                            // Also, skip modification of current quantities
                            if (updatedOrderDetail.IsDeleted)
                            {
                                updatedOrderDetail.State = ObjectState.Deleted;
                                continue;
                            }
                        }
                        else
                        {
                            updatedOrderDetail.State = ObjectState.Added;
                        }

                        // Deduct the correct amount from the item's current quantity
                        this.adjustmentService.ModifyQuantity(QuantityType.CurrentQuantity, orderDetailItem, updatedOrderDetail.Quantity, AdjustmentType.Deduct, Constants.AdjustmentRemarks.OrderUpdated);
                    }
                }

                // Map the entity to an order object
                var order = this.mapper.Map<Order>(entity);

                // Allow EF to track the order object and set the entity state to it's appropriate state
                this.context.ChangeTracker.TrackGraph(order, node => ChangeTrackingHelpers.ConvertStateOfNode(node));

                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="Order"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var order = await this.context.Orders
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
                this.adjustmentService.ModifyQuantity(QuantityType.CurrentQuantity, detail.Item, detail.Quantity, AdjustmentType.Add, Constants.AdjustmentRemarks.OrderDeleted);
            }

            this.context.RemoveRange(order.Details);

            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}