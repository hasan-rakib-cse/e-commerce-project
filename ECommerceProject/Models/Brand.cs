using System.ComponentModel.DataAnnotations;

namespace ECommerceProject.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string BrandName { get; set; }

        [StringLength(50)]
        public string? BrandOrigin { get; set; }


        // Master / Parent

        // Detail / Child
        public List<Product>? Products { get; set; }

    }
}
