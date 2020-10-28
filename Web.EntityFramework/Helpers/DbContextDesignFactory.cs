using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Web.EntityFramework
{  
    public class DbContextDesignFactory : DbContextFactory<ApplicationDbContext>
    {
        public DbContextDesignFactory() : base("DefaultConnection", "Web.EntityFramework")
        { }
        protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
        {
            return new ApplicationDbContext(options);
        }
        
    }
}
