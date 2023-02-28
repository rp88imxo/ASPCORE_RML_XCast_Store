using System.ComponentModel.DataAnnotations;

namespace RMLXCast.Web.ViewModels.Checkout
{
    public class CheckoutProductsViewModel
    {
        public IList<CheckoutViewProductViewModel>? Products { get; set; } = null!;
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [MaxLength(255, ErrorMessage = "Не более {0} символов")]
        public string? OrderNote { get; set; }

        [Required]
        public ShippmentAddress ShippmentAddress { get; set; }   = new ShippmentAddress();
    }
}
