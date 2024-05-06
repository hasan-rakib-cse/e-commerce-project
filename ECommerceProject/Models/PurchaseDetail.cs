using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceProject.Models
{
    public class PurchaseDetail
    {
        public int Id { get; set; }
        [Required]
        public int PurchaseOrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int UnitId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }

        [NotMapped]
        public decimal SubTotal { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        [NotMapped]
        public string UnitName { get; set; }

        // Master / Parent
        public PurchaseOrder? PurchaseOrder { get; set; }
        public Product? Product { get; set; }
        public Unit? Unit { get; set; }
    }
}
