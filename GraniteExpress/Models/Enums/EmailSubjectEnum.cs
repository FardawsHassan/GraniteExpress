using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models.Enums
{
    public enum EmailSubjectEnum
    {
        [Display(Name = "Email Confirmation")]
        EmailConfirmation,
        [Display(Name = "Reset Password Email")]
        ResetPasswordEmail
    }
}
