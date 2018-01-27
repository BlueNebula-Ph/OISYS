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
    using Oisys.Web.Models;

    /// <summary>
    /// <see cref="ProvinceController"/> class handles Province basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ValidateModel]
    public class ProvinceController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<Province, ProvinceSummary> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProvinceController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        public ProvinceController(OisysDbContext context, IMapper mapper, ISummaryListBuilder<Province, ProvinceSummary> builder)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
        }

        /// <summary>
        /// Returns list of active <see cref="Province"/>
        /// </summary>
        /// <param name="filter"><see cref="ProvinceFilterRequest"/></param>
        /// <returns>List of Province</returns>
        [HttpPost("search", Name = "GetAllProvince")]
        public async Task<IActionResult> GetAll([FromBody]ProvinceFilterRequest filter)
        {
            // get list of active sales quote (not deleted)
            var list = this.context.Provinces
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (!string.IsNullOrEmpty(filter?.SearchTerm))
            {
                list = list.Where(c => c.Name.Contains(filter.SearchTerm));
            }

            // sort
            var ordering = $"Name {Helpers.Constants.DefaultSortDirection}";
            if (!string.IsNullOrEmpty(filter?.SortBy))
            {
                ordering = $"{filter.SortBy} {filter.SortDirection}";
            }

            list = list.OrderBy(ordering);

            var entities = await this.builder.BuildAsync(list, filter);

            return this.Ok(entities);
        }

        /// <summary>
        /// Gets a specific <see cref="Province"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Province</returns>
        [HttpGet("{id}", Name = "GetProvince")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.Provinces
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<ProvinceSummary>(entity);

            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="Province"/>.
        /// </summary>
        /// <param name="entity">entity to be created</param>
        /// <returns>Province</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SaveProvinceRequest entity)
        {
            var city = this.mapper.Map<Province>(entity);

            await this.context.Provinces.AddAsync(city);

            await this.context.SaveChangesAsync();

            var mappedProvince = this.mapper.Map<ProvinceSummary>(city);

            return this.CreatedAtRoute("GetProvince", new { id = city.Id }, mappedProvince);
        }

        /// <summary>
        /// Updates a specific <see cref="Province"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody]SaveProvinceRequest entity)
        {
            var city = await this.context.Provinces.SingleOrDefaultAsync(t => t.Id == id);
            if (city == null)
            {
                return this.NotFound(id);
            }

            try
            {
                this.mapper.Map(entity, city);
                this.context.Update(city);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="Province"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var city = await this.context.Provinces
                .SingleOrDefaultAsync(c => c.Id == id);

            if (city == null)
            {
                return this.NotFound(id);
            }

            city.IsDeleted = true;

            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}