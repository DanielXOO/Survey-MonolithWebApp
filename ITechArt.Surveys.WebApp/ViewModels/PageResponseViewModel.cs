using iTechArt.Common.Pagination;
using iTechArt.Surveys.DomainModel;

namespace iTechArt.Surveys.WebApp.ViewModels
{
    public sealed class PageResponseViewModel<T>
    {
        public PagedResult<T> Items { get; set; }

        public string NameSearchTerm { get; set; }

        public SortOrder SortOrder { get; set; }
    }
}