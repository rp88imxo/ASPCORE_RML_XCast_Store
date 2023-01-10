using System.ComponentModel.DataAnnotations;

namespace RMLXCast.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле '{0}' необходимо к заполнению.")]
        [Display(Name = "Логин пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Поле '{0}' необходимо к заполнению.")]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Поле '{0}' необходимо к заполнению.")]
        //[Display(Name = "Дата рождения")]
        //public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Поле '{0}' необходимо к заполнению.")]
        [StringLength(48, ErrorMessage = "Имя не должно иметь больше {0} cимволов")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле '{0}' необходимо к заполнению.")]
        [StringLength(48, ErrorMessage = "Фамилия не должна иметь больше {0} cимволов")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0} необходим к заполнению.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Небходимо потвердить свой пароль.")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        public string PasswordConfirm { get; set; }
    }
}
