using System;

namespace iTechArt.Surveys.WebApp.ViewModels
{
    public class SurveyDeleteViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ReturnUrl { get; set; }
    }
}