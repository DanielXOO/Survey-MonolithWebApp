using System;

namespace iTechArt.Surveys.WebApp.ViewModels
{
    public sealed class UserDeleteViewModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string ReturnUrl { get; set; }
    }
}