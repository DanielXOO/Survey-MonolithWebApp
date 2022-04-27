using System;
using System.Collections.Generic;
using iTechArt.Surveys.DomainModel;

namespace iTechArt.Surveys.WebApp.ViewModels
{
    public class QuestionViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        
        public QuestionType Type { get; set; }

        public ICollection<QuestionOptionsViewModel> Options { get; set; }
    }
}