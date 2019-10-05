﻿namespace PaginableCollections
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This paginable that uses the underlying data source to calculate pagination statistics.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryableAutoMappedBasedPaginable<T, D> : Paginable<D>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemCountPerPage"></param>
        public QueryableAutoMappedBasedPaginable(IQueryable<T> queryable, int pageNumber, int itemCountPerPage, 
            IMapper mapper, Action<IMappingOperationOptions> options = null)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));

            if (itemCountPerPage < 1)
                throw new ArgumentOutOfRangeException(nameof(itemCountPerPage));

            TotalItemCount = queryable?.Count() ?? 0;
            PageNumber = pageNumber;
            ItemCountPerPage = itemCountPerPage;

            if (TotalItemCount > 0)
            {
                innerList.AddRange(queryable
                    .Skip((pageNumber - 1) * ItemCountPerPage)
                    .Take(ItemCountPerPage)
                    .ToPaginableItemList<T, D>(pageNumber, itemCountPerPage, mapper, options));
            }

            if (innerList.Any())
            {
                FirstItemNumber = innerList.First().ItemNumber;
                LastItemNumber = innerList.Last().ItemNumber;
            }
            else
            {
                FirstItemNumber = 0;
                LastItemNumber = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="superset"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemCountPerPage"></param>
        public QueryableAutoMappedBasedPaginable(IEnumerable<T> superset, int pageNumber, int itemCountPerPage, 
            IMapper mapper, Action<IMappingOperationOptions> options)
            : this(superset.AsQueryable(), pageNumber, itemCountPerPage, mapper, options) { }
    }
}