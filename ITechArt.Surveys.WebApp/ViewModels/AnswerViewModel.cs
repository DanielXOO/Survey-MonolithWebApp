using System;
using System.Collections.Generic;

namespace iTechArt.Surveys.WebApp.ViewModels
{
    public sealed class AnswerViewModel
    {
        public Guid SurveyId { get; set; }
        
        public ICollection<QuestionAnswerView> Questions { get; set; }
    }
}