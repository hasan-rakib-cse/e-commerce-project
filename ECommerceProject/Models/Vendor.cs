using System.ComponentModel.DataAnnotations;

namespace ECommerceProject.Models
{
    public class Vendor
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string VendorName { get; set; }

        [StringLength(50)]
        public string? Address { get; set; }


        // Master / Parent

        // Detail / Child
        public List<PurchaseOrder>? Purchases { get; set; }

    }
}
