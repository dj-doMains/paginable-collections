namespace PaginableCollections
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class PaginableExtensions
    {
        /// <summary>
        /// Convert mapped queryable to paginable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="mapper"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemCountPerPage"></param>
        /// <returns></returns>
        public static IPaginable<D> ToPaginable<T, D>(this IQueryable<T> queryable, int pageNumber, int itemCountPerPage, IMapper mapper)
        {
            return new QueryableAutoMappedBasedPaginable<T, D>(queryable, pageNumber, itemCountPerPage, mapper);
        }

        /// <summary>
        /// Convert mapped queryable to paginable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemCountPerPage"></param>
        /// <param name="mapper"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IPaginable<D> ToPaginable<T, D>(this IQueryable<T> queryable, int pageNumber, int itemCountPerPage,
            IMapper mapper, Action<IMappingOperationOptions> options)
        {
            return new QueryableAutoMappedBasedPaginable<T, D>(queryable, pageNumber, itemCountPerPage, mapper, options);
        }

        /// <summary>
        /// Convert mapped queryable to paginable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="paginableInfo"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static IPaginable<D> ToPaginable<T, D>(this IQueryable<T> queryable, IPaginableRequest paginableRequest, IMapper mapper)
        {
            return queryable.ToPaginable<T, D>(paginableRequest.PageNumber, paginableRequest.ItemCountPerPage, mapper);
        }

        /// <summary>
        /// Convert mapped queryable to paginable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="paginableInfo"></param>
        /// <param name="mapper"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IPaginable<D> ToPaginable<T, D>(this IQueryable<T> queryable, IPaginableRequest paginableRequest,
            IMapper mapper, Action<IMappingOperationOptions> options)
        {
            return queryable.ToPaginable<T, D>(paginableRequest.PageNumber, paginableRequest.ItemCountPerPage, mapper, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemCountPerPage"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        internal static IEnumerable<IPaginableItem<D>> ToPaginableItemList<T, D>(this IEnumerable<T> t, int pageNumber, int itemCountPerPage, 
            IMapper mapper, Action<IMappingOperationOptions> options)
        {
            var offset = (pageNumber - 1) * itemCountPerPage;
            var list = t as IList<T> ?? t.ToList();
            for (var i = 0; i < list.Count; i++)
            {
                var item = options == null
                    ? mapper.Map<D>(list[i])
                    : mapper.Map<D>(list[i], options);

                yield return new PaginableItem<D>(item, offset + 1, pageNumber, itemCountPerPage);
                offset++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="mapper"></param>
        /// <param name="pageNumber"></param>
        /// <param name="itemCountPerPage"></param>
        /// <returns></returns>
        internal static IEnumerable<IPaginableItem<D>> ToPaginableItemList<T, D>(this IEnumerable<T> t, int pageNumber, int itemCountPerPage, IMapper mapper)
        {
            return ToPaginableItemList<T, D>(t, pageNumber, itemCountPerPage, null);
        }
    }
}
