using System.ComponentModel.DataAnnotations;

namespace ClipShare.ViewModels.Account
{
    public class Login_vm
    {
        private string _username;
        [Display(Name = "UserName or Email")]
        [Required(ErrorMessage = "UserName is required")]
        public string UserName 
        { 
            get =>_username; 
            set => _username=!string.IsNullOrEmpty(value)?value.ToLower():null; 
        }
        [Required(ErrorMessage = "Password is required")]

        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
