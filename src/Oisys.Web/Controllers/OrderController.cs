namespace Oisys.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BlueNebula.Common.Helpers;
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
                list = list.Where(c => c.Code.ToString().Contains(filter.SearchTerm));
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
        /// Returns list of active <see cref="Order"/>
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <returns>List of Orders per Customer</returns>
        [HttpGet("{customerId}/lookup", Name = "GetOrderLookup")]
        public IActionResult GetLookup(int customerId)
        {
            // get list of active items (not deleted)
            var list = this.context.Orders
                .AsNoTracking()
                .Where(c => !c.IsDeleted && c.CustomerId == customerId)
                .OrderBy(c => c.Code);

            var entities = list.ProjectTo<OrderLookup>();
            return this.Ok(entities);
        }

        /// <summary>
        /// Returns a list of order details
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="isDelivered">True is delivered, false if not</param>
        /// <returns>List of order details per customer</returns>
        [HttpGet("detail/{customerId}/lookup/{isDelivered?}", Name = "GetOrderDetailLookup")]
        public IActionResult GetOrderDetailLookup(int customerId, bool isDelivered = false)
        {
            // get list of active items (not deleted)
            var list = this.context.OrderDetails
                .Include(c => c.Item)
                .ThenInclude(c => c.Category)
                .AsNoTracking()
                .Where(c => !c.Order.IsDeleted && c.Order.CustomerId == customerId);

            if (!isDelivered)
            {
                list = list.Where(c => c.QuantityDelivered != c.Quantity);
            }

            list = list.OrderBy(c => c.Item.Code);

            var entities = list.ProjectTo<OrderDetailLookup>();
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
                        .ThenInclude(e => e.Category)
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

            // deduct items to inventory
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
            var order = await this.context.Orders
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
                foreach (var detail in entity.Details)
                {
                    // if item is delivered, cannot modify order detail
                    if (detail.DeliveryId > 0)
                    {
                        continue;
                    }

                    // get original detail
                    var oldDetail = this.context.OrderDetails.AsNoTracking().SingleOrDefault(c => c.Id == detail.Id);

                    if (oldDetail != null)
                    {
                        // for existing and deleted details
                        if (oldDetail.Quantity != detail.Quantity)
                        {
                            // deduct original quantity
                            this.adjustmentService.ModifyQuantity(QuantityType.CurrentQuantity, oldDetail.Item, detail.Quantity, AdjustmentType.Add, detail.IsDeleted ? Constants.AdjustmentRemarks.OrderDetailDeleted : Constants.AdjustmentRemarks.OrderUpdated);
                        }

                        // deleted existing detail
                        if (detail.IsDeleted)
                        {
                            detail.State = ObjectState.Deleted;
                            continue;
                        }

                        if (oldDetail.Quantity != detail.Quantity)
                        {
                            // add new quantity
                            this.adjustmentService.ModifyQuantity(QuantityType.CurrentQuantity, oldDetail.Item, detail.Quantity, AdjustmentType.Deduct, Constants.AdjustmentRemarks.OrderUpdated);
                        }
                    }

                    // for added details
                    else
                    {
                        var item = this.context.Items.AsNoTracking().SingleOrDefault(c => c.Id == detail.ItemId);

                        this.adjustmentService.ModifyQuantity(QuantityType.CurrentQuantity, item, detail.Quantity, AdjustmentType.Deduct, Constants.AdjustmentRemarks.OrderDetailCreated);
                    }
                }

                order = this.mapper.Map<Order>(entity);

                this.context.Update(order);

                var deletedDetails = order.Details.Where(a => a.State == ObjectState.Deleted);

                this.context.RemoveRange(deletedDetails);

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

            // add back items to inventory
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