﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NerdDinner.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public PaginatedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = source.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            this.AddRange(source.Skip(PageIndex * PageSize).Take(PageSize));
        }

        public bool HasPreviousPage()
        {
            return PageIndex > 0;
        }

        public bool HasNextPage()
        {
            return PageIndex + 1 < TotalPages;
        }
    }
}