using System.ComponentModel.DataAnnotations;

namespace ECommerceProject.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }

        [Required, StringLength(15)]
        public string InvoiceNo { get; set; }

        [Required]
        public decimal TotalPurchasePrice { get; set; }
        public decimal VAT { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetPayableAmount { get; set; }

        // Master / Parent
        [Required]
        public int VendorId { get; set; }
        public Vendor? Vendor { get; set; }
        public List<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
