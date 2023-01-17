using System.ComponentModel.DataAnnotations;

namespace RMLXCast.Web.ViewModels.Product_Category
{
    public class EditProductCategoryViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо указать название категории")]
        [Display(Name = "Название категории")]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
