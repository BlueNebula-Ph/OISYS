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
    using Oisys.Web.DTO;
    using Oisys.Web.Filters;
    using Oisys.Web.Helpers;
    using Oisys.Web.Models;

    /// <summary>
    /// <see cref="CategoryController"/> handles creating, reading, updating and deleting categories
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ValidateModel]
    public class CategoryController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<Category, CategorySummary> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        public CategoryController(
            OisysDbContext context,
            IMapper mapper,
            ISummaryListBuilder<Category, CategorySummary> builder)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
        }

        /// <summary>
        /// Returns list of active <see cref="Category"/>
        /// </summary>
        /// <param name="filter"><see cref="CategoryFilterRequest"/></param>
        /// <returns>List of Category</returns>
        [HttpPost("search", Name = "GetAllCategories")]
        public async Task<IActionResult> GetAll([FromBody] CategoryFilterRequest filter)
        {
            // get list of active categorys (not deleted)
            var list = this.context.Categories
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (!string.IsNullOrEmpty(filter?.SearchTerm))
            {
                list = list.Where(c => c.Name.Contains(filter.SearchTerm));
            }

            // sort
            var ordering = $"Name {Constants.DefaultSortDirection}";
            if (!string.IsNullOrEmpty(filter?.SortBy))
            {
                ordering = $"{filter.SortBy} {filter.SortDirection}";
            }

            list = list.OrderBy(ordering);

            var entities = await this.builder.BuildAsync(list, filter);

            return this.Ok(entities);
        }

        /// <summary>
        /// Returns list of active <see cref="Category"/>
        /// </summary>
        /// <returns>List of Categories</returns>
        [HttpGet("lookup", Name = "GetCategoryLookup")]
        public IActionResult GetLookup()
        {
            // get list of active items (not deleted)
            var list = this.context.Categories
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // sort
            var ordering = $"Name {Constants.DefaultSortDirection}";

            list = list.OrderBy(ordering);

            var entities = list.ProjectTo<CategoryLookup>();

            return this.Ok(entities);
        }

        /// <summary>
        /// Gets a specific <see cref="Category"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Category</returns>
        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<CategorySummary>(entity);

            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="Category"/>.
        /// </summary>
        /// <param name="entity">entity to be created</param>
        /// <returns>Category</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveCategoryRequest entity)
        {
            var category = this.mapper.Map<Category>(entity);
            await this.context.Categories.AddAsync(category);
            await this.context.SaveChangesAsync();

            return this.CreatedAtRoute("GetCategory", new { id = category.Id }, entity);
        }

        /// <summary>
        /// Updates a specific <see cref="Category"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] SaveCategoryRequest entity)
        {
            var category = await this.context.Categories.SingleOrDefaultAsync(t => t.Id == id);
            if (category == null)
            {
                return this.NotFound(id);
            }

            try
            {
                this.mapper.Map(entity, category);
                this.context.Update(category);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="Category"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var category = await this.context.Categories.SingleOrDefaultAsync(t => t.Id == id);
            if (category == null)
            {
                return this.NotFound(id);
            }

            category.IsDeleted = true;
            this.context.Update(category);
            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}