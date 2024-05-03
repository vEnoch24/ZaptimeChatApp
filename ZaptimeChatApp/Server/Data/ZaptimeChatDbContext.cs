using Microsoft.EntityFrameworkCore;
using ZaptimeChatApp.Server.Data.Models;

namespace ZaptimeChatApp.Server.Data
{
    public class ZaptimeChatDbContext : DbContext
    {
        public ZaptimeChatDbContext(DbContextOptions<ZaptimeChatDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>(e =>
            {
                e.HasOne(m => m.ToUser).WithMany().OnDelete(DeleteBehavior.NoAction);
                e.HasOne(m => m.FromUser).WithMany().OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
