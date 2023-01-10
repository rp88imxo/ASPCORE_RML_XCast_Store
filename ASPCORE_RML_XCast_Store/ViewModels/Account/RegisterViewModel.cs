using System.ComponentModel.DataAnnotations;

namespace RMLXCast.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Логин пользователя")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(48, ErrorMessage = "Имя не должно иметь больше {0} cимволов")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(48, ErrorMessage = "Фамилия не должна иметь больше {0} cимволов")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
