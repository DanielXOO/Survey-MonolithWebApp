using System;
using System.Threading.Tasks;
using iTechArt.Common.Pagination;
using iTechArt.Common.Time;
using iTechArt.Surveys.DomainModel;
using iTechArt.Surveys.Repositories;

namespace iTechArt.Surveys.Foundation.Services.Surveys
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveysUnitOfWork _unitOfWork;
        private readonly ISystemClock _systemClock;


        public SurveyService(ISurveysUnitOfWork unitOfWork, ISystemClock systemClock)
        {
            _unitOfWork = unitOfWork;
            _systemClock = systemClock;
        }


        public async Task<PagedResult<Survey>> GetSurveysAsync(int currentPage, int pageSize, 
            SortOrder order, string searchRequest)
        {
            var data = await _unitOfWork.Surveys
                .GetSurveysAsync(pageSize, currentPage, searchRequest, order);

            return data;
        }

        public async Task DeleteSurveyAsync(Survey survey)
        {
            _unitOfWork.Surveys.Delete(survey);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Survey> GetSurveyByIdAsync(Guid id)
        {
            var survey = await _unitOfWork.Surveys.GetByIdAsync(id);

            return survey;
        }

        public async Task AddSurveyAsync(Survey survey, User author)
        {
            survey.Author = author;
            survey.AuthorId = author.Id;
            survey.CreationDate = _systemClock.UtcNow;

            _unitOfWork.Surveys.Create(survey);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateSurveyAsync(Survey survey)
        {
            survey.UpdateDate = _systemClock.UtcNow;
            _unitOfWork.Surveys.Update(survey);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}