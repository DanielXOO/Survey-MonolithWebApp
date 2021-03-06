using System;
using System.Linq;
using iTechArt.Common.Pagination;

namespace iTechArt.Common.Extensions
{
    public static class PagedResultExtensions
    {
        public static  PagedResult<TResult> MapPagedResult<TSource, TResult>(
            this PagedResult<TSource> source,
            Func<TSource, TResult> selector)

        {
            var items = source.Items.Select(selector).ToList();
            var result = new PagedResult<TResult>(items, source.PageSize, source.CurrentPage, source.TotalItems);

            return result;
        }
    }
}