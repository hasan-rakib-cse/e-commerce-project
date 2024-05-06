using System.ComponentModel.DataAnnotations;

namespace ECommerceProject.Models
{
    public class ProductImage
    {
        public int Id { get; set; }

        public string? ImageName { get; set; }

        [Required, StringLength(150)]
        public string ImageCode { get; set; }

        public decimal? ImageSize { get; set; }

        public string? ImageExtension { get; set; }

        [Required]
        // Master / Parent
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        // Detail / Child
    }
}
