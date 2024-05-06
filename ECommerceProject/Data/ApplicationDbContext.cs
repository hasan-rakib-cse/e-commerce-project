using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ECommerceProject.Models;

namespace ECommerceProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ECommerceProject.Models.Brand> Brand { get; set; } = default!;
        public DbSet<ECommerceProject.Models.Category> Category { get; set; } = default!;
        public DbSet<ECommerceProject.Models.Configaration> Configaration { get; set; } = default!;
        public DbSet<ECommerceProject.Models.Customer> Customer { get; set; } = default!;
        public DbSet<ECommerceProject.Models.Product> Product { get; set; } = default!;
        public DbSet<ECommerceProject.Models.ProductImage> ProductImage { get; set; } = default!;
        public DbSet<ECommerceProject.Models.ProductModel> ProductModel { get; set; } = default!;
        public DbSet<ECommerceProject.Models.PurchaseOrder> Purchase { get; set; } = default!;
        public DbSet<ECommerceProject.Models.SalesOrder> SalesOrder { get; set; } = default!;
        public DbSet<ECommerceProject.Models.SaleDetails> SaleDetails { get; set; } = default!;
        public DbSet<ECommerceProject.Models.Stock> Stock { get; set; } = default!;
        public DbSet<ECommerceProject.Models.SubCategory> SubCategory { get; set; } = default!;
        public DbSet<ECommerceProject.Models.Unit> Unit { get; set; } = default!;
        public DbSet<ECommerceProject.Models.Vendor> Vendor { get; set; } = default!;
        public DbSet<ECommerceProject.Models.PurchaseDetail> PurchaseDetail { get; set; } = default!;
    }
}
