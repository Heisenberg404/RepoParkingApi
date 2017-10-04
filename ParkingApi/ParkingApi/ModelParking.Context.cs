namespace ParkingApi
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class ParkingEntities : DbContext
    {
        public ParkingEntities()
            : base("name=ParkingEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Floor> Floor { get; set; }
        public virtual DbSet<ParkCells> ParkCells { get; set; }
        public virtual DbSet<Price> Price { get; set; }
        public virtual DbSet<Record> Record { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }
        public virtual DbSet<UserMonthPayment> UserMonthPayments { get; set; }
    }
}