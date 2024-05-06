using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace ECommerceProject.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string ProductName { get; set; }

        [StringLength(200)]
        public string? ShortDescription { get; set; }

        [StringLength(500)]
        public string? LongDescription { get; set; }

        [Required, StringLength(15)]
        public string ProductCode { get; set; }

        [Required]
        public decimal SalePrice { get; set; }
       
        public decimal Weight { get; set; }

        public decimal WarnPoint { get; set; }
        public int ProductWarranty { get; set; }

        public decimal VAT { get; set; }
        public bool DiscountInPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [Required]
        // Master / Parent
        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }

        [Required]
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }

        [Required]
        public int UnitId { get; set; }
        public Unit? Unit { get; set; }

        [Required]
        public int ProductModelId { get; set; }
        public ProductModel? ProductModel { get; set; }

        [Required, Display(Name = "Configaration Name")]
        public int ConfigarationId { get; set; }
        public Configaration? Configaration { get; set; }


        // Detail / Child
        public List<ProductImage>? ProductImages { get; set; }
        public List<Stock>? Stocks { get; set; }
        public List<PurchaseDetail>? PurchaseDetail { get; set; }
        public List<SaleDetails>? SaleDetails { get; set; }
    }
}
