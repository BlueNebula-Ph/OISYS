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
    using Oisys.Web;
    using Oisys.Web.DTO;
    using Oisys.Web.Filters;
    using Oisys.Web.Models;

    /// <summary>
    /// <see cref="CityController"/> class handles City basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ValidateModel]
    public class CityController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<City, CitySummary> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="CityController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        public CityController(OisysDbContext context, IMapper mapper, ISummaryListBuilder<City, CitySummary> builder)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
        }

        /// <summary>
        /// Returns list of active <see cref="City"/>
        /// </summary>
        /// <param name="filter"><see cref="CityFilterRequest"/></param>
        /// <returns>List of City</returns>
        [HttpPost("search", Name = "GetAllCity")]
        public async Task<IActionResult> GetAll([FromBody]CityFilterRequest filter)
        {
            // get list of active sales quote (not deleted)
            var list = this.context.Cities
                .Include(c => c.Province)
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (!string.IsNullOrEmpty(filter?.SearchTerm))
            {
                list = list.Where(c => c.Name.Contains(filter.SearchTerm));
            }

            if (!(filter?.ProvinceId).IsNullOrZero())
            {
                list = list.Where(c => c.ProvinceId == filter.ProvinceId);
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
        /// Gets a specific <see cref="City"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>City</returns>
        [HttpGet("{id}", Name = "GetCity")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.Cities
                .AsNoTracking()
                .Include(c => c.Province)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<CitySummary>(entity);

            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="City"/>.
        /// </summary>
        /// <param name="entity">entity to be created</param>
        /// <returns>City</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SaveCityRequest entity)
        {
            var city = this.mapper.Map<City>(entity);

            await this.context.Cities.AddAsync(city);

            await this.context.SaveChangesAsync();

            var mappedCity = this.mapper.Map<CitySummary>(city);

            return this.CreatedAtRoute("GetCity", new { id = city.Id }, mappedCity);
        }

        /// <summary>
        /// Updates a specific <see cref="City"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody]SaveCityRequest entity)
        {
            var city = await this.context.Cities.SingleOrDefaultAsync(t => t.Id == id);
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
        /// Deletes a specific <see cref="City"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var city = await this.context.Cities
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