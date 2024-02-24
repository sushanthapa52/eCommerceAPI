using eCommerceClassLib.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

        // DbSet for the Product model
        public DbSet<Product> Products { get; set; }

        // DbSet for the Category model
        public DbSet<Category> Categories { get; set; }

        // DbSet for the ShoppingCart model
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

      




    }
}
