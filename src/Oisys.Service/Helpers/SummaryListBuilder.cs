﻿namespace Oisys.Service.Helpers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The summary list builder
    /// </summary>
    /// <typeparam name="T">The entity type of the source list</typeparam>
    /// <typeparam name="T1">The result type of the summary list</typeparam>
    public class SummaryListBuilder<T, T1> : ISummaryListBuilder<T, T1>
    {
        /// <summary>
        /// Builds the summary list
        /// </summary>
        /// <param name="source">The source queryable</param>
        /// <param name="pageNumber">The page number</param>
        /// <param name="pageSize">The page size</param>
        /// <returns>The summary list result</returns>
        public async Task<SummaryList<T1>> BuildAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (source == null)
            {
                throw new ArgumentException(nameof(source));
            }

            var totalRecords = await source.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            var items = source
                .Page(pageNumber, pageSize)
                .ProjectTo<T1>();

            return new SummaryList<T1>()
            {
                TotalPages = totalPages,
                PageIndex = pageNumber,
                Items = items,
            };
        }
    }
}