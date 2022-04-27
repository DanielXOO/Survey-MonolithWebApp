using System.ComponentModel.DataAnnotations;

namespace iTechArt.Surveys.WebApp.ViewModels
{
    public class UserRegistrationViewModel
    {
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Login cannot be empty")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repeat password")]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }
    }
}