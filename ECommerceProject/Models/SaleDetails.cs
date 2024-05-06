using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceProject.Models
{
    public class SaleDetails
    {
        public int Id { get; set; }
        [Required]
        public int SalesOrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int UnitId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SaleQuantity { get; set; }
        
        [NotMapped]
        public decimal SubTotal { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        [NotMapped]
        public string UnitName { get; set; }

        // Master / Parent
        public SalesOrder? SalesOrder { get; set; }
        public Product? Product { get; set; }
        public Unit? Unit { get; set; }
    }
}
