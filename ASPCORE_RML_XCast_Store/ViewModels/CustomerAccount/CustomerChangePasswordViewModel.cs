using System.ComponentModel.DataAnnotations;

namespace RMLXCast.Web.ViewModels.CustomerAccount
{
    public class CustomerChangePasswordViewModel
    {
        [Required(ErrorMessage ="{0} необходим к заполнению")]
        [Display(Name ="Текущий пароль")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [Compare("NewPasswordRepeat", ErrorMessage = "Пароли не совпадают!")]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают!")]
        public string NewPasswordRepeat { get; set; } = string.Empty;
    }
}
