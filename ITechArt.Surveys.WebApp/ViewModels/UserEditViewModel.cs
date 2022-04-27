using System;

namespace iTechArt.Surveys.WebApp.ViewModels
{
    public class UserEditViewModel
    {
        public string Name { get; set; }

        public Guid Id { get; set; }

        public string ReturnUrl { get; set; }
    }
}