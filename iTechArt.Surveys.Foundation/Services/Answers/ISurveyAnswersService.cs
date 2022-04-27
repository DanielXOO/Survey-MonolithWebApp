using System;
using System.Threading.Tasks;
using iTechArt.Surveys.DomainModel;

namespace iTechArt.Surveys.Foundation.Services.Answers
{
    public interface ISurveyAnswersService
    {
        Task<SurveyAnswer> GetAnswerByIdAsync(Guid id);

        Task AddAnswerAsync(SurveyAnswer answer, User author);
    }
}