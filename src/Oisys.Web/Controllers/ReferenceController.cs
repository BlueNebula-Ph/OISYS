namespace Oisys.Web.Controllers
{
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Oisys.Web.DTO;
    using Oisys.Web.Models;

    /// <summary>
    /// <see cref="ReferenceController"/> class handles ReferenceType basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReferenceController : Controller
    {
        private readonly OisysDbContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// /// <param name="mapper">Automapper</param>
        public ReferenceController(OisysDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns list of active <see cref="Reference"/>
        /// </summary>
        /// <param name="id">Reference type id</param>
        /// <returns>List of References</returns>
        [HttpGet("{id}/lookup", Name = "GetReferenceLookup")]
        public IActionResult GetLookup(int id)
        {
            // get list of active items (not deleted)
            var list = this.context.References
                .AsNoTracking()
                .Where(c => c.ReferenceTypeId == id)
                .OrderBy(c => c.Code);

            var entities = list.ProjectTo<ReferenceLookup>();
            return this.Ok(entities);
        }
    }
}