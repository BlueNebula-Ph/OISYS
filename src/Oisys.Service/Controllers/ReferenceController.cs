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
    /// <see cref="ReferenceController"/> class handles Reference basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReferenceController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<Reference, ReferenceSummary> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        public ReferenceController(OisysDbContext context, IMapper mapper, ISummaryListBuilder<Reference, ReferenceSummary> builder)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
        }

        /// <summary>
        /// Returns list of active <see cref="Reference"/>
        /// </summary>
        /// <param name="typeId">ReferenceType Id</param>
        /// <param name="filter">Reference filter</param>
        /// <returns>List of References</returns>
        [HttpPost("{typeId}/search", Name = "GetAllReferences")]
        public async Task<IActionResult> GetAll(long typeId, [FromBody]ReferenceFilterRequest filter)
        {
            // get list of active references (not deleted)
            var list = this.context.References
                .AsNoTracking()
                .Where(c => !c.IsDeleted && c.ReferenceTypeId == typeId);

            // filter
            if (!string.IsNullOrEmpty(filter?.SearchTerm))
            {
                list = list.Where(c => c.Code.Contains(filter.SearchTerm) || (c.ParentReference != null && c.ParentReference.Code.Contains(filter.SearchTerm)));
            }

            if (!(filter?.ParentId).IsNullOrZero())
            {
                list = list.Where(c => c.ParentReferenceId == filter.ParentId);
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
        /// Returns list of active <see cref="Reference"/>
        /// </summary>
        /// <param name="typeId">ReferenceType Id</param>
        /// <param name="parentId">Parent Id</param>
        /// <returns>List of References</returns>
        [HttpGet("{typeId}/lookup/{parentId?}", Name = "GetReferenceLookup")]
        public IActionResult GetLookup(long typeId, int? parentId)
        {
            // get list of active references (not deleted)
            var list = this.context.References
                .AsNoTracking()
                .Where(c => !c.IsDeleted && c.ReferenceTypeId == typeId);

            // filter
            if (!parentId.IsNullOrZero())
            {
                list = list.Where(c => c.ParentReferenceId == parentId);
            }

            // sort
            var ordering = $"Code {Constants.DefaultSortDirection}";

            list = list.OrderBy(ordering);

            var entities = list.ProjectTo<ReferenceLookup>();

            return this.Ok(entities);
        }

        /// <summary>
        /// Gets a specific <see cref="Reference"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Reference</returns>
        [HttpGet("{id}", Name = "GetReference")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.References
                .AsNoTracking()
                .Include(c => c.ReferenceType)
                .Include(c => c.ParentReference)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<ReferenceSummary>(entity);

            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="Reference"/>.
        /// </summary>
        /// <param name="entity">Reference to save</param>
        /// <returns>Reference</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveReferenceRequest entity)
        {
            if (entity == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var reference = this.mapper.Map<Reference>(entity);
            await this.context.References.AddAsync(reference);
            await this.context.SaveChangesAsync();

            var mappedReference = this.mapper.Map<ReferenceSummary>(reference);

            return this.CreatedAtRoute("GetReference", new { id = reference.Id }, mappedReference);
        }

        /// <summary>
        /// Updates a specific <see cref="Reference"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody]SaveReferenceRequest entity)
        {
            if (entity == null || entity.Id == 0 || id == 0)
            {
                return this.BadRequest();
            }

            var reference = await this.context.References.SingleOrDefaultAsync(t => t.Id == id);
            if (reference == null)
            {
                return this.NotFound(id);
            }

            try
            {
                this.mapper.Map(entity, reference);
                this.context.Update(reference);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="Reference"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var entity = await this.context.References.SingleOrDefaultAsync(t => t.Id == id);
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
