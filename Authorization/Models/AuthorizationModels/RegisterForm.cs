using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace Authorization.Models.AuthorizationModels
{
    public class RegisterForm: Form
    {
        [Required(ErrorMessage = "Wrong nickname")]
        public string Nickname { get; set; }
        [EmailAddress(ErrorMessage = "Wrong email")]
        [Required(ErrorMessage = "Wrong email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Wrong password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Error confirm password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        public override bool Validate()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            if (!Validator.TryValidateObject(this, context, results, true))
            {
                foreach (var error in results)
                {
                    MessageBox.Show(error.ErrorMessage, 
                        "Register error",
                        MessageBoxButton.OK, 
                        MessageBoxImage.Error);
                    return false;
                }
                return true;
            }
            else return true;
        }

    }
}