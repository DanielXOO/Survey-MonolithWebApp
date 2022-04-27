using System;

namespace iTechArt.Surveys.DomainModel
{
    public sealed class QuestionOption
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Question Question { get; set; }
        
        public Guid QuestionId { get; set; }
    }
}