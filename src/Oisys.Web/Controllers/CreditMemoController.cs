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
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (!string.IsNullOrEmpty(filter?.SearchTerm))
            {
                list = list.Where(c => c.Code.ToString().Contains(filter.SearchTerm));
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
                list = list.Where(c => c.Details.Any(d => d.OrderDetail.ItemId == filter.ItemId));
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
                .Include(c => c.Customer)
                .Include(c => c.Details)
                    .ThenInclude(d => d.OrderDetail.Item)
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

                    // total amount to deduct from customer's balance
                    totalAmountReturned = totalAmountReturned + (orderDetail.Price * detail.Quantity);

                    // update quantity returned and check if it exceeds quantity
                    orderDetail.QuantityReturned += detail.Quantity;

                    if (orderDetail.QuantityReturned > orderDetail.Quantity)
                    {
                        this.ModelState.AddModelError(Constants.ErrorMessage, $"Total quantity returned for {item.Name} cannot be greater than {orderDetail.Quantity}");
                        return this.BadRequest(this.ModelState);
                    }

                    // add checking if order detail is delivered

                    // add back to inventory
                    if (detail.ShouldAddBackToInventory)
                    {
                        this.adjustmentService.ModifyQuantity(QuantityType.Both, item, detail.Quantity, AdjustmentType.Add, Constants.AdjustmentRemarks.CreditMemoCreated);
                    }
                }

                // Add customer transaction
                var customerTransaction = this.customerService.AddCustomerTransaction(entity.CustomerId, TransactionType.Credit, totalAmountReturned, Constants.AdjustmentRemarks.CreditMemoCreated);

                await this.context.CreditMemos.AddAsync(creditMemo);

                customerTransaction.CreditMemoId = creditMemo.Id;

                await this.context.SaveChangesAsync();

                return this.CreatedAtRoute("GetCreditMemo", new { id = creditMemo.Id }, entity);
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
            var cm = this.context.CreditMemos
                        .AsNoTracking()
                        .Include(c => c.Customer)
                        .Include(c => c.Details)
                        .Include("Details.OrderDetail.Item")
                        .SingleOrDefault(c => c.Id == id);

            if (cm == null)
            {
                return this.NotFound(id);
            }

            decimal totalAmountToDeduct = 0;
            decimal totalAmountToAdd = 0;

            try
            {
                foreach (var detail in entity.Details)
                {
                    // get updated detail
                    var oldDetail = this.context.CreditMemoDetails
                                        .Include(c => c.OrderDetail)
                                        .AsNoTracking()
                                        .SingleOrDefault(c => c.Id == detail.Id);

                    if (oldDetail != null)
                    {
                        // for existing and deleted details
                        if (oldDetail.Quantity != detail.Quantity)
                        {
                            // deduct original quantity
                            this.adjustmentService.ModifyQuantity(QuantityType.CurrentQuantity, oldDetail.OrderDetail.Item, detail.Quantity, AdjustmentType.Deduct, detail.IsDeleted ? Constants.AdjustmentRemarks.CreditMemoDetailDeleted : Constants.AdjustmentRemarks.CreditMemoUpdated);
                            this.adjustmentService.ModifyQuantity(QuantityType.ActualQuantity, oldDetail.OrderDetail.Item, detail.Quantity, AdjustmentType.Deduct, detail.IsDeleted ? Constants.AdjustmentRemarks.CreditMemoDetailDeleted : Constants.AdjustmentRemarks.CreditMemoUpdated);

                            // Amount to add to customer balance
                            totalAmountToAdd = totalAmountToAdd + (oldDetail.OrderDetail.Price * detail.Quantity);
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
                            this.adjustmentService.ModifyQuantity(QuantityType.CurrentQuantity, oldDetail.OrderDetail.Item, detail.Quantity, AdjustmentType.Add, Constants.AdjustmentRemarks.CreditMemoUpdated);
                            this.adjustmentService.ModifyQuantity(QuantityType.ActualQuantity, oldDetail.OrderDetail.Item, detail.Quantity, AdjustmentType.Add, Constants.AdjustmentRemarks.CreditMemoUpdated);

                            // Deduct amount from Customer Account
                            totalAmountToDeduct = totalAmountToDeduct + (oldDetail.OrderDetail.Price * oldDetail.Quantity);
                        }
                    }

                    // for added details
                    else
                    {
                        var orderDetail = this.context.OrderDetails
                                                .Include(c => c.Item)
                                                .AsNoTracking()
                                                .SingleOrDefault(c => c.Id == detail.OrderDetailId);

                        this.adjustmentService.ModifyQuantity(QuantityType.Both, orderDetail.Item, detail.Quantity, AdjustmentType.Deduct, Constants.AdjustmentRemarks.CreditMemoDetailCreated);

                        // Deduct amount from Customer Account
                        totalAmountToDeduct = totalAmountToDeduct + (orderDetail.Price * detail.Quantity);
                    }
                }

                // get customer transaction
                var customerTransaction = this.context.CustomerTransactions
                                                .SingleOrDefault(c => c.CustomerId == cm.CustomerId && c.CreditMemoId == cm.Id);

                // update customer transaction record
                if (customerTransaction != null)
                {
                    this.customerService.ModifyCustomerTransaction(customerTransaction, TransactionType.Credit, totalAmountToAdd, Constants.AdjustmentRemarks.CreditMemoUpdated);
                    this.customerService.ModifyCustomerTransaction(customerTransaction, TransactionType.Debit, totalAmountToDeduct, Constants.AdjustmentRemarks.CreditMemoUpdated);
                }

                cm = this.mapper.Map<CreditMemo>(entity);

                this.context.Update(cm);

                var deletedDetails = cm.Details.Where(a => a.State == ObjectState.Deleted);

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
        /// Deletes a specific <see cref="CreditMemo"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var creditMemo = await this.context.CreditMemos
                                .Include(c => c.Details)
                                .Include("Details.OrderDetail.Item")
                                .AsNoTracking()
                                .SingleOrDefaultAsync(c => c.Id == id);

            if (creditMemo == null)
            {
                return this.NotFound(id);
            }

            try
            {
                // Delete credit memo
                creditMemo.IsDeleted = true;

                decimal totalAmountReturnedToBalance = 0;

                foreach (var detail in creditMemo.Details)
                {
                    this.adjustmentService.ModifyQuantity(QuantityType.Both, detail.OrderDetail.Item, detail.Quantity, AdjustmentType.Deduct, Constants.AdjustmentRemarks.CreditMemoDeleted);

                    // compute amount to add to customer's balance
                    totalAmountReturnedToBalance = totalAmountReturnedToBalance + (detail.OrderDetail.Price * detail.Quantity);
                }

                // Remove credit memo details
                this.context.RemoveRange(creditMemo.Details);

                // get customer transaction
                var customerTransaction = this.context.CustomerTransactions
                                                .SingleOrDefault(c => c.CustomerId == creditMemo.CustomerId && c.CreditMemoId == creditMemo.Id);

                if (customerTransaction != null)
                {
                    this.customerService.ModifyCustomerTransaction(customerTransaction, TransactionType.Debit, totalAmountReturnedToBalance, Constants.AdjustmentRemarks.CreditMemoDeleted);
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
