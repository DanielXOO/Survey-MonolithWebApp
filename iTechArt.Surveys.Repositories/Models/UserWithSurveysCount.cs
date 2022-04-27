using iTechArt.Surveys.DomainModel;

namespace iTechArt.Surveys.Repositories.Models
{
    public sealed class UserWithSurveysCount
    {
        public User User { get; set; }

        public int SurveysCount { get; set; }
    }
}