using System.ComponentModel.DataAnnotations;

namespace RMLXCast.Web.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(48)]
        public string Name { get; set; }
       
        [MaxLength(512)]
        public string? ShortDescription { get; set; }

        [MaxLength(2048)]
        public string? FullDescription { get; set; }
       
        [MaxLength(128)]
        public string? AdminComment { get; set; }

        public bool AllowCustomerReviews { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int OrderMinimumQuantity { get; set; }

        [Required]
        public int OrderMaximumQuantity { get; set; }

        public bool Published { get; set; }

        //Stock
        [Required]
        [Display(Name = "Доступное количество товара")]
        public int StockQuantity { get; set; }

    }
}
