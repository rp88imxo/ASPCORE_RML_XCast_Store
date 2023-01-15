using Microsoft.AspNetCore.Identity;
using RMLXCast.Core.Domain.Role;
using System.ComponentModel.DataAnnotations;

namespace RMLXCast.Web.ViewModels.User
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле '{0}' необходимо к заполнению!")]
        [Display(Name ="Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле '{0}' необходимо к заполнению!")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        public bool IsBanned { get; set; }

        public IList<ApplicationUserRole> AllRoles { get; set; } = new List<ApplicationUserRole>();

        public IList<string> UserRoles { get; set; } = new List<string>();
    }
}
