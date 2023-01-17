using System.ComponentModel.DataAnnotations;

namespace RMLXCast.Web.ViewModels
{
    public class PagedProductCategoryViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Категория должна иметь имя.")]
        [Display(Name= "Название категории")]
        public string Name { get; set; }

        [Display(Name = "Краткое описание категории")]
        public string? Description { get; set; }

        public bool Editable { get; set; }

    }
}
