using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using dtWebApi.Models;

namespace dtWebApi.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Settings> Settings { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new SettingsConfiguration());
            // Other configurations...
        }
    }
}
