namespace Oisys.Service.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Oisys.Service.DTO;
    using Oisys.Service.Helpers;

    /// <summary>
    /// <see cref="OrderController"/> class handles Order basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<Order, OrderSummary> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        public OrderController(OisysDbContext context, IMapper mapper, ISummaryListBuilder<Order, OrderSummary> builder)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
        }

        /// <summary>
        /// Returns list of active <see cref="Order"/>
        /// </summary>
        /// <param name="filter"><see cref="OrderFilterRequest"/></param>
        /// <returns>List of Orders</returns>
        [HttpPost("search", Name = "GetAllOrders")]
        public async Task<IActionResult> GetAll([FromBody]OrderFilterRequest filter)
        {
            // get list of active customers (not deleted)
            var list = this.context.Orders
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (!string.IsNullOrEmpty(filter?.SearchTerm))
            {
                list = list.Where(c => c.Code.Contains(filter.SearchTerm));
            }

            if (!(filter?.CustomerId).IsNullOrZero())
            {
                list = list.Where(c => c.CustomerId == filter.CustomerId);
            }

            if (filter?.Date != null)
            {
                list = list.Where(c => c.Date >= filter.Date && c.Date <= filter.Date);
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

            // paging
            var pageNumber = (filter?.PageIndex).IsNullOrZero() ? Constants.DefaultPageIndex : filter.PageIndex;
            var pageSize = (filter?.PageSize).IsNullOrZero() ? Constants.DefaultPageSize : filter.PageSize;

            var entities = await this.builder.BuildAsync(list, pageNumber, pageSize);

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
            if (entity == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var order = this.mapper.Map<Order>(entity);
            await this.context.Orders.AddAsync(order);
            await this.context.SaveChangesAsync();

            var mappedOrder = this.mapper.Map<OrderSummary>(entity);

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
            if (entity == null || entity.Id == 0 || id == 0)
            {
                return this.BadRequest();
            }

            var order = await this.context.Orders.SingleOrDefaultAsync(t => t.Id == id);
            if (order == null)
            {
                return this.NotFound(id);
            }

            try
            {
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
        /// Deletes a specific <see cref="Order"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var order = await this.context.Orders.SingleOrDefaultAsync(t => t.Id == id);
            if (order == null)
            {
                return this.NotFound(id);
            }

            order.IsDeleted = true;
            this.context.Update(order);
            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}