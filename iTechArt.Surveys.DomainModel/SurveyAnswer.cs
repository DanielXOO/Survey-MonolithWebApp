using System;
using System.Collections.Generic;

namespace iTechArt.Surveys.DomainModel
{
    public sealed class SurveyAnswer
    {
        public Guid Id { get; set; }
        
        public User User { get; set; }
        
        public Guid UserId { get; set; }
        
        public Guid SurveyId { get; set; }

        public Survey Survey { get; set; }
        
        public ICollection<QuestionAnswer> QuestionAnswers { get; set; }
    }
}