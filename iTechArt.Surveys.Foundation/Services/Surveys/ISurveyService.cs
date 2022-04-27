using System;
using System.Threading.Tasks;
using iTechArt.Common.Pagination;
using iTechArt.Surveys.DomainModel;

namespace iTechArt.Surveys.Foundation.Services.Surveys
{
    public interface ISurveyService
    {
        Task<PagedResult<Survey>> GetSurveysAsync(int currentPage, int pageSize,
            SortOrder order, string searchRequest);

        Task DeleteSurveyAsync(Survey survey);

        Task<Survey> GetSurveyByIdAsync(Guid id);

        Task AddSurveyAsync(Survey survey, User author);

        Task UpdateSurveyAsync(Survey survey);
    }
}