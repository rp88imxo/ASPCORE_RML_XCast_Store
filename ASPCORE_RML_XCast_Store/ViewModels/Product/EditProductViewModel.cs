using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RMLXCast.Web.ViewModels.Product
{
    public class EditProductViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле '{0}' необходимо к заполнению.")]
        [Display(Name = "Название товара")]
        [MaxLength(255, ErrorMessage = "Поле '{0}' не должно превышать {1} символов.")]
        public string Name { get; set; }

        [Display(Name = "Короткое описание")]
        [MaxLength(1024, ErrorMessage = "Поле '{0}' не должно превышать {1} символов.")]
        public string? ShortDescription { get; set; }

        [Display(Name = "Полное описание")]
        [MaxLength(4096, ErrorMessage = "Поле '{0}' не должно превышать {1} символов.")]
        public string? FullDescription { get; set; }

        [Display(Name = "Комментарий админа")]
        [MaxLength(1024, ErrorMessage = "Поле '{0}' не должно превышать {1} символов.")]
        public string? AdminComment { get; set; }

        public bool AllowCustomerReviews { get; set; }

        [Required(ErrorMessage = "Поле '{0}' необходимо к заполнению.")]
        [Display(Name = "Цена товара")]
        public decimal Price { get; set; }

        [Display(Name = "Минимальное количество товара для оформления заказа")]
        public int OrderMinimumQuantity { get; set; } = 1;

        [Display(Name = "Максимальное количество товара для оформления заказа")]
        public int OrderMaximumQuantity { get; set; } = 999;

        [Display(Name = "Опубликован ли товар")]
        public bool Published { get; set; }

        [Display(Name = "Доступное количество")]
        public int Stock { get; set; }

        [Display(Name = "Изображения товара")]
        public List<IFormFile>? ProductImages { get; set; }

        public IList<SelectListItem> AllProductCategoriesSelectListItems { get; set; } = new List<SelectListItem>();
        public IList<int> SelectedProductCategoriesIds { get; set; } = new List<int>();
    }
}
