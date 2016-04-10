﻿namespace PaginableCollections
{
    using System;
    using System.Linq;

    public static class PaginableExtensions
    {
        /// <summary>
        /// Convert queryable to paginable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemCountPerPage"></param>
        /// <returns></returns>
        public static IPaginable<T> ToPaginable<T>(this IQueryable<T> queryable, int pageNumber, int itemCountPerPage)
        {
            return new QueryableBasedPaginable<T>(queryable, pageNumber, itemCountPerPage);
        }

        /// <summary>
        /// Convert queryable to paginable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paginableInfo"></param>
        /// <returns></returns>
        public static IPaginable<T> ToPaginable<T>(this IQueryable<T> queryable, IPaginableInfo paginableInfo)
        {
            return queryable.ToPaginable(paginableInfo.PageNumber, paginableInfo.ItemCountPerPage);
        }
    }
}