﻿namespace Oisys.Service.Controllers
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
    /// <see cref="ItemController"/> class handles Item basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<Item, ItemSummary> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        public ItemController(OisysDbContext context, IMapper mapper, ISummaryListBuilder<Item, ItemSummary> builder)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
        }

        /// <summary>
        /// Returns list of active <see cref="Item"/>
        /// </summary>
        /// <param name="filter"><see cref="ItemFilterRequest"/></param>
        /// <returns>List of Items</returns>
        [HttpPost("search", Name = "GetAllItems")]
        public async Task<IActionResult> GetAll([FromBody]ItemFilterRequest filter)
        {
            // get list of active items (not deleted)
            var list = this.context.Items
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (!string.IsNullOrEmpty(filter?.SearchTerm))
            {
                list = list.Where(c => c.Code.Contains(filter.SearchTerm));
            }

            if (!(filter?.CategoryId).IsNullOrZero())
            {
                list = list.Where(c => c.CategoryId == filter.CategoryId);
            }

            // TODO: filter price

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
        /// Returns list of active <see cref="Item"/>
        /// </summary>
        /// <returns>List of Items</returns>
        [HttpGet("lookup", Name = "GetItemLookup")]
        public IActionResult GetLookup()
        {
            // get list of active items (not deleted)
            var list = this.context.Items
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // sort
            var ordering = $"Code {Constants.DefaultSortDirection}";

            list = list.OrderBy(ordering);

            var entities = list.ProjectTo<ItemLookup>();

            return this.Ok(entities);
        }

        /// <summary>
        /// Gets a specific <see cref="Item"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Item</returns>
        [HttpGet("{id}", Name = "GetItem")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.Items
                .AsNoTracking()
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<ItemSummary>(entity);

            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="Item"/>.
        /// </summary>
        /// <param name="entity">entity to be created</param>
        /// <returns>Item</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SaveItemRequest entity)
        {
            if (entity == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var item = this.mapper.Map<Item>(entity);
            await this.context.Items.AddAsync(item);
            await this.context.SaveChangesAsync();

            try
            {
                var mappedItem = this.mapper.Map<ItemSummary>(item);

                return this.CreatedAtRoute("GetItem", new { id = item.Id }, mappedItem);
            }
            catch (AutoMapperMappingException ex)
            {

            }

            return this.Ok();
        }

        /// <summary>
        /// Updates a specific <see cref="Item"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody]SaveItemRequest entity)
        {
            if (entity == null || entity.Id == 0 || id == 0)
            {
                return this.BadRequest();
            }

            var item = await this.context.Items.SingleOrDefaultAsync(t => t.Id == id);
            if (item == null)
            {
                return this.NotFound(id);
            }

            try
            {
                this.mapper.Map(entity, item);
                this.context.Update(item);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="Item"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var entity = await this.context.Items.SingleOrDefaultAsync(t => t.Id == id);
            if (entity == null)
            {
                return this.NotFound(id);
            }

            entity.IsDeleted = true;
            this.context.Update(entity);
            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}