using System.ComponentModel.DataAnnotations;

namespace RMLXCast.Web.ViewModels.Cart
{
    public class CartProductViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
