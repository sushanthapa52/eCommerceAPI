using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }
    }
}
