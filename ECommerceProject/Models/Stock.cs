using System.ComponentModel.DataAnnotations;

namespace ECommerceProject.Models
{
    public class Stock
    {
        public int Id { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime StockDate { get; set; }
        public decimal StockIn {  get; set; }
        public decimal StockOut { get; set; }
        public decimal StockBalance { get; set; }

        // Master / Parent
        [Required]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        // Detail / Child
    }
}
