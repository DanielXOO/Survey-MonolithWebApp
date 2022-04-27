using System;
using System.Threading.Tasks;
using iTechArt.Common.Pagination;
using iTechArt.Repositories;
using iTechArt.Surveys.DomainModel;

namespace iTechArt.Surveys.Repositories.Repositories
{
    public interface ISurveyRepository : IRepository<Survey>
    {
        Task<PagedResult<Survey>> GetSurveysAsync(int pageSize, int currentPage,
            string searchRequest, SortOrder sortOrder);
        
        Task<Survey> GetByIdAsync(Guid id);
    }
}