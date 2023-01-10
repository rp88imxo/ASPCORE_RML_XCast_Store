using System.ComponentModel.DataAnnotations;

namespace RMLXCast.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} необходимо к заполнению.")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} необходимо к заполнению.")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
