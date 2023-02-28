using System.ComponentModel.DataAnnotations;

namespace RMLXCast.Web.ViewModels.Checkout
{
    public class ShippmentAddress
    {
        public int Id { get; set; } = -1;

        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address1 { get; set; }

        public string? Address2 { get; set; }
        [Required]
        public string? ZipPostalCode { get; set; }
    }
}
