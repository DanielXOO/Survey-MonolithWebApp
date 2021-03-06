using System;
using System.Collections.Generic;

namespace iTechArt.Surveys.DomainModel
{
    public sealed class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public string PasswordHash { get; set; }

        public DateTime CreationTime { get; set; }

        public ICollection<Role> Roles { get; set; }

        public ICollection<Survey> Surveys { get; set; }

        public ICollection<SurveyAnswer> Answers { get; set; }
    }
}