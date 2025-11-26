using Microsoft.EntityFrameworkCore;

namespace AccomodationBookingMVC.Models
{
    public partial class AccommodationContext : DbContext
    {
        public AccommodationContext() { }

        public AccommodationContext(DbContextOptions<AccommodationContext> options)
            : base(options) { }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<PlatformEmployee> PlatformEmployees { get; set; }
        public DbSet<Premise> Premises { get; set; }
        public DbSet<PremiseType> PremiseTypes { get; set; }
        public DbSet<RentalAgreement> RentalAgreements { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RentalAgreement>()
                .Property(r => r.TotalSum)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Tenant>()
                .Property(t => t.MaxPrice)
                .HasPrecision(18, 2);
        }
    }
}
