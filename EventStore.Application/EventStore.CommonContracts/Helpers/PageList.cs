using System;
using System.Collections.Generic;
using System.Linq;

namespace EventStore.CommonContracts.Helpers
{
    public class PagesList<DType> : List<DType> where DType : class
    {
        /// <summary>
        /// Current page
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// The whole amount of page
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// Item per page
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Count
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// Whether is previous page
        /// </summary>
        public bool HasPrevious => CurrentPage > 1;

        /// <summary>
        /// Whether is next page
        /// </summary>
        public bool HasNext => CurrentPage < TotalPages;

        /// <summary>
        /// Parametrized constructor
        /// </summary>
        /// <param name="items">IEnumerable{DataType}</param>
        /// <param name="count">int</param>
        /// <param name="pageNumber">int</param>
        /// <param name="pageSize">int</param>
        public PagesList(IEnumerable<DType> items, int count,
            int pageNumber, int pageSize)
        {
            TotalCount = count;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        /// <summary>
        /// Init the collection by default
        /// </summary>
        /// <param name="source">IQueryable{DataType}</param>
        /// <param name="pageNumber">int</param>
        /// <param name="pageSize">int</param>
        /// <returns></returns>
        public static PagesList<DType> Init(IEnumerable<DType> source,
            int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagesList<DType>(items, count, pageNumber, pageSize);
        }
    }
}
