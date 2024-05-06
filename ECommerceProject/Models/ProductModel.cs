using System.ComponentModel.DataAnnotations;

namespace ECommerceProject.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string ModelName { get; set; }


        // Master / Parent

        // Detail / Child
        public List<Product>? Products { get; set; }

    }
}
