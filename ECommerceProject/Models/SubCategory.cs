using System.ComponentModel.DataAnnotations;

namespace ECommerceProject.Models
{
    public class SubCategory
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string SubCategoryName { get; set; }

        // Master / Parent
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // Detail / Child
        public List<Product>? Products { get; set; }

    }
}
