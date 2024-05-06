using System.ComponentModel.DataAnnotations;

namespace ECommerceProject.Models
{
    public class Configaration
    {
        public int Id { get; set; }

        [Required, StringLength(300)]
        public string ConfigarationName { get; set; }


        // Master / Parent

        // Detail / Child
        public List<Product>? Products { get; set; }

    }
}
