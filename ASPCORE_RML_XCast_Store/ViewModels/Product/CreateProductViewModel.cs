using RMLXCast.Core.Domain.Catalog;
using System.ComponentModel.DataAnnotations;

namespace RMLXCast.Web.ViewModels.Product
{
    public class CreateProductViewModel
    {
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
		public int OrderMinimumQuantity { get; set; } = 0;

		[Display(Name = "Максимальное количество товара для оформления заказа")]
		public int OrderMaximumQuantity { get; set; } = 25;

		[Display(Name = "Опубликован ли товар")]
		public bool Published { get; set; }

		[Display(Name = "Доступное количество")]
		public int Stock { get; set; }

        [Display(Name = "Изображения товара")]
        public IFormFileCollection? ProductImages { get; set; } 

		public IList<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
	}
}
