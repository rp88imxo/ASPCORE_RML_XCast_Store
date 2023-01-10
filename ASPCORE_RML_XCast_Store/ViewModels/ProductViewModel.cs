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
        [Required]
        [MaxLength(128)]
        public string? Description { get; set; }

    }
}
