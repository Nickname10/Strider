using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Authorization.Models.AuthorizationModels
{
    public class LoginForm : Form
    {
        [Required(ErrorMessage = "Wrong email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Wrong password")]
        public string Password { get; set; }
       
    }
}