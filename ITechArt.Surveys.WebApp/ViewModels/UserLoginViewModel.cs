using System.ComponentModel.DataAnnotations;

namespace iTechArt.Surveys.WebApp.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Login cannot be empty")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }
    }
}