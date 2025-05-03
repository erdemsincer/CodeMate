using Microsoft.EntityFrameworkCore;
using ReviewService.Entities;

namespace ReviewService.Data
{
    public class ReviewDbContext : DbContext
    {
        public ReviewDbContext(DbContextOptions<ReviewDbContext> options) : base(options)
        {
        }

        public DbSet<Review> Reviews => Set<Review>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Content).IsRequired().HasMaxLength(1000);
                entity.Property(r => r.Rating).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
