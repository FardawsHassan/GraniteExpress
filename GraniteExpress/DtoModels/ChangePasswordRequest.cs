using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.DtoModels
{
    public class ChangePasswordRequest
    {
        [Required(ErrorMessage = "Current Password is required")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("NewPassword", ErrorMessage = "New Password and Confirmation Password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
