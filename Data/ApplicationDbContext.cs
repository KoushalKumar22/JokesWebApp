using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JokesWebApp.Models;

namespace JokesWebApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Joke> Joke { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Joke>(entity =>
            {
                entity.HasOne(j => j.Creator)
                      .WithMany()
                      .HasForeignKey(j => j.CreatorId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(j => j.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasIndex(j => j.CreatorId);
                entity.HasIndex(j => j.CreatedAt);
            });
        }
    }
}
