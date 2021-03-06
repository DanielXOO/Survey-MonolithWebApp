using System;
using System.Collections;
using System.Collections.Generic;

namespace iTechArt.Surveys.DomainModel
{
    public sealed class Survey
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public User Author { get; set; }

        public Guid AuthorId { get; set; }
        
        public ICollection<Question> Questions { get; set; }

        public ICollection<SurveyAnswer> Answers { get; set; }
    }
}