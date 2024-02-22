using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data
{
    public class AppSecurityContext : IdentityDbContext<IdentityUser>
    {
        public AppSecurityContext(DbContextOptions<AppSecurityContext> options) :
            base(options)
        { }
    }
}
