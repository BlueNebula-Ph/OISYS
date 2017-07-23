namespace Oisys.Service.Helpers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// <see cref="QueryableExtensions"/> class to handle custom query.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Returns sorted list
        /// </summary>
        /// <param name="source">List to sort</param>
        /// <param name="sortBy">property to sort by</param>
        /// <param name="sortDirection">sort direction of the collection</param>
        /// <param name="values">values</param>
        /// <returns>Returns sorted collection</returns>
        /// <typeparam name="T">generic collection</typeparam>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortBy, string sortDirection, params object[] values)
        {
            // get Type of collection
            var type = typeof(T);

            if (type != null)
            {
                var property = type.GetProperty(sortBy);
                if (property != null)
                {
                    var parameter = Expression.Parameter(type, "c");
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);
                    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), sortDirection == Constants.SortDirectionAscending ? "OrderBy" : "OrderByDescending", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
                    return source.Provider.CreateQuery<T>(resultExp);
                }
            }

            // if sort didn't work, return original collection
            return source;
        }
    }
}
