using System.ComponentModel.DataAnnotations;

namespace ECommerceProject.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string CategoryName { get; set; }


        // Master / Parent

        // Detail / Child
        public List<SubCategory>? SubCategories { get; set; }
    }
}
