using System.ComponentModel.DataAnnotations;

namespace ECommerceProject.Models
{
    public class SalesOrder
    {
        public int Id { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }

        [Required, StringLength(15)]
        public string InvoiceNumber { get; set; }
        public decimal TotalBillAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal VAT { get; set; }
        public decimal ShippingCharge { get; set; }
        public decimal NetPayableBill { get; set; }

        [StringLength(300)]
        public string? Remarks { get; set; }

        [Required]
        // Master / Parent
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        // Detail / Child
        public List<SaleDetails>? SaleDetails { get; set; }
    }
}
