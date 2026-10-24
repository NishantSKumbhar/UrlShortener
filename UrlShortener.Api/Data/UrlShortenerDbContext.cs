using Microsoft.EntityFrameworkCore;
using UrlShortener.Api.Domain.Entities;

namespace UrlShortener.Api.Data
{
    public class UrlShortenerDbContext:DbContext
    {
        public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options):base(options)
        {
            
        }


        public DbSet<UrlMapping> UrlMappings => Set<UrlMapping>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UrlMapping>(entity =>
            {
                entity.ToTable("UrlMappings");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.OriginalUrl)
                    .IsRequired()
                    .HasMaxLength(2048);

                entity.Property(x => x.ShortCode)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.HasIndex(x => x.ShortCode)
                    .IsUnique()
                    .HasDatabaseName("UX_UrlMappings_ShortCode");

                //entity.HasIndex(x => x.OriginalUrl)
                //    .IsUnique()
                //    .HasDatabaseName("UX_UrlMappings_OriginalUrl");

                entity.Property(x => x.OriginalUrlHash)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasIndex(x => x.OriginalUrlHash)
                    .IsUnique()
                    .HasDatabaseName("UX_UrlMappings_OriginalUrlHash");




                entity.Property(x => x.CreatedAtUtc)
                    .IsRequired();
            });
        }
    }
}
