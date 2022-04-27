using System;
using System.Threading.Tasks;
using iTechArt.Repositories;
using iTechArt.Surveys.DomainModel;

namespace iTechArt.Surveys.Repositories.Repositories
{
    public interface IAnswerRepository : IRepository<SurveyAnswer>
    {
        Task<SurveyAnswer> GetByIdAsync(Guid id);
    }
}