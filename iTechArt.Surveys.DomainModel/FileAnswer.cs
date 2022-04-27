using System;

namespace iTechArt.Surveys.DomainModel
{
    public sealed class FileAnswer
    {
        public Guid Id { get; set; }
        
        public FileInfo FileInfo { get; set; }

        public Guid QuestionAnswerId { get; set; }

        public QuestionAnswer QuestionAnswer { get; set; }
    }
}