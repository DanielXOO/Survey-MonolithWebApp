using System;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.Repositories.EFCore;
using iTechArt.Surveys.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Surveys.Repositories.Repositories
{
    public class AnswerRepository : Repository<SurveyAnswer>, IAnswerRepository
    {
        public AnswerRepository(DbContext context) : base(context)
        {
        }
        

        public async Task<SurveyAnswer> GetByIdAsync(Guid id)
        {
            var answers = await GetAnswersQuery()
                .FirstOrDefaultAsync(answer => answer.Id == id);

            return answers;
        }


        private IQueryable<SurveyAnswer> GetAnswersQuery()
        {
            return Data
                .Include(answer => answer.QuestionAnswers);
        }
    }
}