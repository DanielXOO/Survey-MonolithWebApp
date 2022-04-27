using System;
using System.Collections.Generic;

namespace iTechArt.Surveys.WebApp.ViewModels
{
    public class SurveyViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public ICollection<QuestionViewModel> Questions { get; set; }

    }
}