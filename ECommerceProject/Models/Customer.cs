using System.ComponentModel.DataAnnotations;

namespace ECommerceProject.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string CustomerName { get; set; }

		[StringLength(15)]
        public string? CustomerMobile { get; set; }

        [Required, EmailAddress]
        public string CustomerEmail { get; set; }

        [StringLength(50)]
        public string? CustomerAddress { get; set; }



        // Master / Parent
        
        // Detail / Child
        public List<SalesOrder>? SalesOrders { get; set; }

    }
}
