namespace Oisys.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper;
    using BlueNebula.Common.Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Oisys.Web.DTO;
    using Oisys.Web.Exceptions;
    using Oisys.Web.Filters;
    using Oisys.Web.Helpers;
    using Oisys.Web.Services.Interfaces;

    /// <summary>
    /// <see cref="DeliveryController"/> class handles Delivery basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ValidateModel]
    public class DeliveryController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<Delivery, DeliverySummary> builder;
        private readonly IAdjustmentService adjustmentService;
        private readonly ICustomerService customerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        /// <param name="adjustmentService">Adjustment Service</param>
        /// <param name="customerService">Customer Service</param>
        public DeliveryController(OisysDbContext context, IMapper mapper, ISummaryListBuilder<Delivery, DeliverySummary> builder, IAdjustmentService adjustmentService, ICustomerService customerService)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
            this.adjustmentService = adjustmentService;
            this.customerService = customerService;
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
                list = list.Where(c => c.DeliveryNumber.ToString().Contains(filter.SearchTerm));
            }

            if (!(filter?.CustomerId).IsNullOrZero())
            {
                list = list.Where(c => c.Details.Any(a => a.OrderDetail.Order.CustomerId == filter.CustomerId));
            }

            if (filter?.DateFrom != null || filter?.DateTo != null)
            {
                filter.DateFrom = filter.DateFrom == null || filter.DateFrom == DateTime.MinValue ? DateTime.Today : filter.DateFrom;
                filter.DateTo = filter.DateTo == null || filter.DateTo == DateTime.MinValue ? DateTime.Today : filter.DateTo;
                list = list.Where(c => c.Date >= filter.DateFrom && c.Date < filter.DateTo.Value.AddDays(1));
            }

            if (!(filter?.ItemId).IsNullOrZero())
            {
                list = list.Where(c => c.Details.Any(d => d.OrderDetail.ItemId == filter.ItemId));
            }

            // sort
            var ordering = $"DeliveryNumber {Constants.DefaultSortDirection}";
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
                .Include(c => c.Province)
                .Include(c => c.City)
                .Include(c => c.Details)
                .Include("Details.OrderDetail")
                .Include("Details.OrderDetail.Order.Customer")
                .Include("Details.OrderDetail.Item")
                .Include("Details.OrderDetail.Item.Category")
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
            try
            {
                var delivery = this.mapper.Map<Delivery>(entity);
                var customerTransactions = new Dictionary<int, decimal>();

                foreach (var detail in entity.Details)
                {
                    // Fetch the order detail associated
                    var orderDetail = await this.context.OrderDetails
                        .Include(a => a.Order)
                            .ThenInclude(a => a.Customer)
                        .Include(a => a.Item)
                        .SingleOrDefaultAsync(a => a.Id == detail.OrderDetailId);

                    this.adjustmentService.ModifyQuantity(QuantityType.ActualQuantity, orderDetail.Item, detail.Quantity, AdjustmentType.Deduct, Constants.AdjustmentRemarks.DeliveryCreated);

                    var customerId = orderDetail.Order.CustomerId;
                    var amount = detail.Quantity * orderDetail.Price; // Todo: add discount here.
                    if (customerTransactions.Keys.Contains(customerId))
                    {
                        customerTransactions[customerId] += amount;
                    }
                    else
                    {
                        customerTransactions.Add(customerId, amount);
                    }

                    orderDetail.QuantityDelivered += detail.Quantity;

                    if (orderDetail.QuantityDelivered > orderDetail.Quantity)
                    {
                        throw new QuantityDeliveredException($"Total quantity delivered for {orderDetail.Item.Name} cannot exceed {orderDetail.Quantity}.");
                    }
                }

                // Add customer transaction
                foreach (var transaction in customerTransactions)
                {
                    this.customerService.AddCustomerTransaction(transaction.Key, TransactionType.Debit, transaction.Value, Constants.AdjustmentRemarks.DeliveryCreated);
                }

                await this.context.Deliveries.AddAsync(delivery);

                await this.context.SaveChangesAsync();

                return this.CreatedAtRoute("GetDelivery", new { id = delivery.Id }, entity);
            }
            catch (QuantityDeliveredException dEx)
            {
                this.ModelState.AddModelError(Constants.ErrorMessage, dEx.Message);
                return this.BadRequest(this.ModelState);
            }
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
            var deliveryExists = await this.context.Deliveries
                .AnyAsync(c => c.Id == id);

            if (!deliveryExists)
            {
                return this.NotFound(id);
            }

            try
            {
                // Adjust actual quantities accordingly
                //foreach (var updatedDeliveryDetail in entity.Details)
                //{
                //    var deliveryDetailItem = await this.context.Items.FindAsync(updatedDeliveryDetail.ItemId);
                //    if (deliveryDetailItem != null)
                //    {
                //        // Fetch the old detail as no tracking to not interfere with the context-tracked order detail
                //        var oldDetail = await this.context.OrderDetails
                //            .AsNoTracking()
                //            .SingleOrDefaultAsync(c => c.Id == updatedDeliveryDetail.Id);

                //        // If the order detail exists, return the old quantities back
                //        if (oldDetail != null)
                //        {
                //            this.adjustmentService.ModifyQuantity(QuantityType.ActualQuantity, deliveryDetailItem, oldDetail.Quantity, AdjustmentType.Add, Constants.AdjustmentRemarks.DeliveryUpdated);

                //            // If DeliveryId is set to 0, do not re-add the actual quantity
                //            if (updatedDeliveryDetail.DeliveryId == 0)
                //            {
                //                continue;
                //            }
                //        }

                //        // Deduct the correct amount from the item's current quantity
                //        this.adjustmentService.ModifyQuantity(QuantityType.ActualQuantity, deliveryDetailItem, updatedDeliveryDetail.Quantity, AdjustmentType.Deduct, Constants.AdjustmentRemarks.DeliveryUpdated);
                //    }
                //}

                // Map the entity to an delivery object
                var delivery = this.mapper.Map<Delivery>(entity);

                this.context.Update(delivery);

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
            var delivery = await this.context.Deliveries
                .Include(c => c.Details)
                .Include("Details.Item")
                .SingleOrDefaultAsync(c => c.Id == id);

            if (delivery == null)
            {
                return this.NotFound(id);
            }

            delivery.IsDeleted = true;

            foreach (var detail in delivery.Details)
            {
                this.adjustmentService.ModifyQuantity(QuantityType.ActualQuantity, detail.OrderDetail.Item, detail.Quantity, AdjustmentType.Add, Constants.AdjustmentRemarks.DeliveryDeleted);
                detail.DeliveryId = 0;
            }

            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}