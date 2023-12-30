using Microsoft.EntityFrameworkCore;
using SalesManagement.Shared.Models;

namespace SalesManagement.Server.DatabaseAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Window> Windows { get; set; }
        public DbSet<SubElement> SubElements { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Window>()
              .HasOne(w => w.Order)
              .WithMany(o => o.windows)
              .HasForeignKey(w => w.OrderId);

            modelBuilder.Entity<SubElement>()
                .HasOne(s => s.Window)
                .WithMany(w => w.subelements)
                .HasForeignKey(s => s.WindowId);
        }
    }
}
