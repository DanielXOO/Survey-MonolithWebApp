using System;

namespace iTechArt.Surveys.DomainModel
{
    public class QuestionAnswerOption
    {
        public Guid Id { get; set; }
        
        public Guid QuestionAnswerId { get; set; }

        public QuestionAnswer QuestionAnswer { get; set; }

        public Guid QuestionOptionId { get; set; }
    }
}