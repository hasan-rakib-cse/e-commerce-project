using System.ComponentModel.DataAnnotations;

namespace ECommerceProject.Models
{
    public class Unit
    {
        public int Id { get; set; }

        [Required, StringLength(15)]
        public string UnitName { get; set; }

        // Master / Parent
        public List<Product>? Product { get; set; }
        public List<PurchaseDetail>? PurchaseDetail { get; set; }
        public List<SaleDetails>? SaleDetails { get; set; }
    }
}
