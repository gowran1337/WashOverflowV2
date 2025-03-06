using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WashOverflowV2.Models;

namespace WashOverflowV2.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<PackageFeature> PackageFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<PackageFeature>()
                .HasKey(pf => new { pf.PackageId, pf.FeatureId });

            modelBuilder.Entity<PackageFeature>()
                .HasOne(pf => pf.Package)
                .WithMany(p => p.PackageFeatures)
                .HasForeignKey(pf => pf.PackageId);

            modelBuilder.Entity<PackageFeature>()
                .HasOne(pf => pf.Feature)
                .WithMany(f => f.PackageFeatures)
                .HasForeignKey(pf => pf.FeatureId);
        }
    }
}
