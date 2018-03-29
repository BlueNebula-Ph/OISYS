﻿namespace Oisys.Web.Controllers
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
    using Oisys.Web.Services.Interfaces;

    /// <summary>
    /// <see cref="CreditMemoController"/> class handles CreditMemo basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ValidateModel]
    public class CreditMemoController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<CreditMemo, CreditMemoSummary> builder;
        private readonly IAdjustmentService adjustmentService;
        private readonly ICustomerService customerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditMemoController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        /// <param name="adjustmentService">Adjustment Service</param>
        /// <param name="customerService">CustomerService</param>
        public CreditMemoController(OisysDbContext context, IMapper mapper, ISummaryListBuilder<CreditMemo, CreditMemoSummary> builder, IAdjustmentService adjustmentService, ICustomerService customerService)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
            this.adjustmentService = adjustmentService;
            this.customerService = customerService;
        }

        /// <summary>
        /// Returns list of active <see cref="CreditMemo"/>
        /// </summary>
        /// <param name="filter"><see cref="CreditMemoFilterRequest"/></param>
        /// <returns>List of CreditMemos</returns>
        [HttpPost("search", Name = "GetAllCreditMemos")]
        public async Task<IActionResult> GetAll([FromBody]CreditMemoFilterRequest filter)
        {
            // get list of active creditMemos (not deleted)
            var list = this.context.CreditMemos
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
            var creditMemoing = $"Code {Constants.DefaultSortDirection}";
            if (!string.IsNullOrEmpty(filter?.SortBy))
            {
                creditMemoing = $"{filter.SortBy} {filter.SortDirection}";
            }

            list = list.OrderBy(creditMemoing);

            var entities = await this.builder.BuildAsync(list, filter);

            return this.Ok(entities);
        }

        /// <summary>
        /// Gets a specific <see cref="CreditMemo"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>CreditMemo</returns>
        [HttpGet("{id}", Name = "GetCreditMemo")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.CreditMemos
                .AsNoTracking()
                .Include(c => c.Customer)
                .Include(c => c.CustomerTransaction)
                .Include(c => c.Details)
                    .ThenInclude(d => d.Item)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<CreditMemoSummary>(entity);

            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="CreditMemo"/>.
        /// </summary>
        /// <param name="entity">entity to be created</param>
        /// <returns>CreditMemo</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SaveCreditMemoRequest entity)
        {
            try
            {
                var creditMemo = this.mapper.Map<CreditMemo>(entity);
                decimal totalAmountReturned = 0;

                foreach (var detail in entity.Details)
                {
                    var item = await this.context.Items.FindAsync(detail.ItemId);

                    var orderDetail = await this.context.OrderDetails.FindAsync(detail.OrderDetailId);

                    // add back to inventory
                    if (detail.ShouldAddBackToInventory)
                    {
                        this.adjustmentService.ModifyQuantity(QuantityType.ActualQuantity, item, detail.Quantity, AdjustmentType.Add, Constants.AdjustmentRemarks.CreditMemoCreated);
                    }

                    // total amount to deduct from customer's balance
                    totalAmountReturned = totalAmountReturned + (orderDetail.Price * detail.Quantity);
                }

                // Add customer transaction
                this.customerService.AddCustomerTransaction(entity.CustomerId, creditMemo, AdjustmentType.Deduct, totalAmountReturned, Constants.AdjustmentRemarks.CreditMemoCreated);

                // Deduct amount from customer balance
                var customer = await this.context.Customers.FindAsync(entity.CustomerId);
                if (customer != null)
                {
                    customer.Balance = customer.Balance - totalAmountReturned;
                }

                await this.context.CreditMemos.AddAsync(creditMemo);

                await this.context.SaveChangesAsync();

                var mappedCreditMemo = this.mapper.Map<CreditMemoSummary>(creditMemo);

                return this.CreatedAtRoute("GetCreditMemo", new { id = creditMemo.Id }, mappedCreditMemo);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        /// <summary>
        /// Updates a specific <see cref="CreditMemo"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody]SaveCreditMemoRequest entity)
        {
            // get credit memo
            var creditMemoExists = await this.context.CreditMemos
                .AsNoTracking()
                .Include(c => c.CustomerTransaction)
                .AnyAsync(c => c.Id == id);

            var customer = await this.context.Customers
                .SingleOrDefaultAsync(c => c.Id == entity.CustomerId);

            if (!creditMemoExists)
            {
                return this.NotFound(id);
            }

            try
            {
                // Set the state to modified
                entity.State = ObjectState.Modified;
                decimal totalAmountToDeduct = 0;
                decimal totalAmountToAdd = 0;

                // Adjust current quantities accordingly
                foreach (var updatedCreditMemoDetail in entity.Details)
                {
                    var creditMemoDetailItem = await this.context.Items.FindAsync(updatedCreditMemoDetail.ItemId);

                    var orderDetail = await this.context.OrderDetails.FindAsync(updatedCreditMemoDetail.OrderDetailId);

                    if (creditMemoDetailItem != null)
                    {
                        // Fetch the old detail as no tracking to not interfere with the context-tracked creditMemo detail
                        var oldDetail = await this.context.CreditMemoDetails
                            .AsNoTracking()
                            .SingleOrDefaultAsync(c => c.Id == updatedCreditMemoDetail.Id);

                        // If the creditMemo detail exists, return the old quantities back
                        if (oldDetail != null)
                        {
                            updatedCreditMemoDetail.State = ObjectState.Modified;

                            // Deduct items from Inventory
                            this.adjustmentService.ModifyQuantity(QuantityType.ActualQuantity, creditMemoDetailItem, oldDetail.Quantity, AdjustmentType.Deduct, Constants.AdjustmentRemarks.CreditMemoUpdated);

                            // Amount to add to customer balance
                            totalAmountToAdd = totalAmountToAdd + (oldDetail.Quantity * orderDetail.Price);

                            // If a delete is issued to the creditMemo detail, remove that creditMemo detail
                            // Also, skip modification of current quantities
                            if (updatedCreditMemoDetail.IsDeleted)
                            {
                                updatedCreditMemoDetail.State = ObjectState.Deleted;
                                continue;
                            }
                        }
                        else
                        {
                            updatedCreditMemoDetail.State = ObjectState.Added;
                        }

                        // Add the correct amount from the item's current quantity
                        this.adjustmentService.ModifyQuantity(QuantityType.ActualQuantity, creditMemoDetailItem, updatedCreditMemoDetail.Quantity, AdjustmentType.Add, Constants.AdjustmentRemarks.CreditMemoUpdated);

                        // Deduct amount from Customer Account
                        totalAmountToDeduct = totalAmountToDeduct + (orderDetail.Price * updatedCreditMemoDetail.Quantity);
                    }
                }

                // Map the entity to an creditMemo object
                var updatedCreditMemo = this.mapper.Map<CreditMemo>(entity);

                // Allow EF to track the creditMemo object and set the entity state to it's appropriate state
                this.context.ChangeTracker.TrackGraph(updatedCreditMemo, node => ChangeTrackingHelpers.ConvertStateOfNode(node));

                this.customerService.ModifyCustomerTransaction(updatedCreditMemo.CustomerTransactionId, AdjustmentType.Deduct, totalAmountToDeduct, Constants.AdjustmentRemarks.CreditMemoUpdated);

                if (customer != null)
                {
                    customer.Balance = customer.Balance + totalAmountToAdd;
                    customer.Balance = customer.Balance - totalAmountToDeduct;
                }

                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="CreditMemo"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var creditMemo = await this.context.CreditMemos
                .Include(c => c.Customer)
                .Include(c => c.CustomerTransaction)
                .Include(c => c.Details)
                .Include("Details.Item")
                .SingleOrDefaultAsync(c => c.Id == id);

            if (creditMemo == null)
            {
                return this.NotFound(id);
            }

            try
            {
                OrderDetail orderDetail = null;

                // Delete credit memo
                creditMemo.IsDeleted = true;
                decimal totalAmountReturnedToBalance = 0;

                foreach (var detail in creditMemo.Details)
                {
                    orderDetail = await this.context.OrderDetails.FindAsync(detail.OrderDetailId);

                    this.adjustmentService.ModifyQuantity(QuantityType.CurrentQuantity, detail.Item, detail.Quantity, AdjustmentType.Deduct, Constants.AdjustmentRemarks.CreditMemoDeleted);

                    // compute amount to add to customer's balance
                    totalAmountReturnedToBalance = totalAmountReturnedToBalance + (orderDetail.Price * detail.Quantity);
                }

                // Remove credit memo details
                this.context.RemoveRange(creditMemo.Details);

                // Delete customer transaction attached to credit memo
                this.customerService.DeleteCustomerTransaction(creditMemo.CustomerTransaction);

                var customer = await this.context.Customers
                        .SingleOrDefaultAsync(c => c.Id == creditMemo.CustomerId);

                // Update customer's balance
                if (customer != null)
                {
                    creditMemo.Customer.Balance = creditMemo.Customer.Balance + totalAmountReturnedToBalance;
                }

                await this.context.SaveChangesAsync();

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
    }
}
