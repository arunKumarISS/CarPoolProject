namespace CarPool.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class CarPoolEntities : DbContext
    {
        public CarPoolEntities()
            : base("name=CarPoolEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public DbSet<BookingTable> Bookings { get; set; }
        public DbSet<LocationTable> Locations { get; set; }
        public DbSet<OfferRequestTable> OfferRequests { get; set; }
        public virtual DbSet<OfferTable> Offers { get; set; }
        public virtual DbSet<PaymentTable> Payments { get; set; }
        public virtual DbSet<UserTable> Users { get; set; }
        public virtual DbSet<ViaPointsTable> ViaPoints { get; set; }
    }
}