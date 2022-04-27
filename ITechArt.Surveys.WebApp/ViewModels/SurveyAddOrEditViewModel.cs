using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iTechArt.Surveys.WebApp.ViewModels
{
    public sealed class SurveyAddOrEditViewModel
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public ICollection<QuestionCreateOrEditViewModel> Questions { get; set; }
    }
}