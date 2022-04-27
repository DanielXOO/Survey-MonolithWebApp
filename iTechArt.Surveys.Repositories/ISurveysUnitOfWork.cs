using iTechArt.Repositories;
using iTechArt.Surveys.Repositories.Repositories;

namespace iTechArt.Surveys.Repositories
{
    public interface ISurveysUnitOfWork : IUnitOfWork
    {
        IUserRepository Users { get; }

        IRoleRepository Roles { get; }

        IAnswerRepository Answers { get; }

        ISurveyRepository Surveys { get; }

        IFileRepository Files { get; }
    }
}