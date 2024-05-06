using ECommerceProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceProject.ViewModel
{
    public class ProductVM
    {
        public int Id { get; set; }

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

        [NotMapped]
        public string Image { get; set; }

        [NotMapped]
        public string ConfigarationName { get; set; }

        public Product? Product { get; set; }
        public Unit? Unit { get; set; }

        [Required, Display(Name = "Configaration Name")]
        public int ConfigarationId { get; set; }
        public Configaration? Configaration { get; set; }

    }
}
