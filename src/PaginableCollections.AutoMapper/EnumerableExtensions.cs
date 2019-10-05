namespace PaginableCollections
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static IPaginable<D> ToPaginable<T, D>(this IEnumerable<T> enumerable, int pageNumber, int itemCountPerPage, IMapper mapper)
        {
            return
                enumerable
                    .AsQueryable()
                    .ToPaginable<T, D>(pageNumber, itemCountPerPage, mapper);
        }

        public static IPaginable<D> ToPaginable<T, D>(this IEnumerable<T> enumerable,  IPaginableRequest paginableRequest, IMapper mapper)
        {
            return
                enumerable
                    .ToPaginable<T, D>(paginableRequest.PageNumber, paginableRequest.ItemCountPerPage, mapper);
        }

        public static IPaginable<D> ToPaginable<T, D>(this IEnumerable<T> enumerable, int pageNumber, int itemCountPerPage, IMapper mapper, Action<IMappingOperationOptions> options)
        {
            return
                enumerable
                    .AsQueryable()
                    .ToPaginable<T, D>(pageNumber, itemCountPerPage, mapper, options);
        }

        public static IPaginable<D> ToPaginable<T, D>(this IEnumerable<T> enumerable, IPaginableRequest paginableRequest, IMapper mapper, Action<IMappingOperationOptions> options)
        {
            return
                enumerable
                    .ToPaginable<T, D>(paginableRequest.PageNumber, paginableRequest.ItemCountPerPage, mapper, options);
        }
    }
}
