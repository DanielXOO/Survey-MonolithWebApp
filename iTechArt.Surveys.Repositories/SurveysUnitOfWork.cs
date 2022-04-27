using iTechArt.Repositories.EFCore;
using iTechArt.Surveys.DomainModel;
using iTechArt.Surveys.Repositories.Repositories;

namespace iTechArt.Surveys.Repositories
{
    public sealed class SurveysUnitOfWork : UnitOfWork, ISurveysUnitOfWork
    {
        public IUserRepository Users
            => (IUserRepository) GetRepository<User>();

        public IRoleRepository Roles
            => (IRoleRepository) GetRepository<Role>();

        public ISurveyRepository Surveys 
            => (ISurveyRepository) GetRepository<Survey>();

        public IFileRepository Files
            => (IFileRepository) GetRepository<FileInfo>();

        public IAnswerRepository Answers
            => (IAnswerRepository) GetRepository<SurveyAnswer>();
        
        
        public SurveysUnitOfWork(SurveysDbContext dbContext)
            : base(dbContext)
        {
            AddSpecificRepository<User, UserRepository>();
            AddSpecificRepository<Role, RoleRepository>();
            AddSpecificRepository<Survey, SurveyRepository>();
            AddSpecificRepository<SurveyAnswer, AnswerRepository>();
            AddSpecificRepository<FileInfo, FileRepository>();
        }
    }
}