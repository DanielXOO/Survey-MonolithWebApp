using System;
using System.Threading.Tasks;
using iTechArt.Surveys.DomainModel;
using iTechArt.Surveys.Repositories;

namespace iTechArt.Surveys.Foundation.Services.Answers
{
    public class SurveySurveyAnswersService : ISurveyAnswersService
    {
        private readonly ISurveysUnitOfWork _unitOfWork;
        
        
        public SurveySurveyAnswersService(ISurveysUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        
        public async Task<SurveyAnswer> GetAnswerByIdAsync(Guid id)
        {
            var answer = await _unitOfWork.Answers.GetByIdAsync(id);

            return answer;
        }

        public async Task AddAnswerAsync(SurveyAnswer answer, User author)
        {
            answer.User = author;
            answer.UserId = author.Id;
            
            _unitOfWork.Answers.Create(answer);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}