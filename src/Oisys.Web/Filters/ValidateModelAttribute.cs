namespace Oisys.Web.Filters
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Oisys.Web.Helpers;

    /// <summary>
    /// Filter to validate the model and return model errors
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Validates the model
        /// </summary>
        /// <param name="context">The action executing context</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(a => a.Errors.Select(b => b.ErrorMessage));
                context.ModelState.AddModelError(Constants.ErrorMessage, string.Join(" ", errors));
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}