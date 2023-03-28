using System.ComponentModel.DataAnnotations;

namespace RMLXCast.Web.ViewModels.CustomerAccount
{
    public class CustomerChangePasswordViewModel
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [Compare("NewPasswordRepeat", ErrorMessage = "Пароли не совпадают!")]
        public string NewPassword { get; set; }

        [Required]
        public string NewPasswordRepeat { get; set; }
    }
}
