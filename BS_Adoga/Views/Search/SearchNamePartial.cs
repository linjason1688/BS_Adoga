using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BS_Adoga.Views.Search
{
    public partial class SearchNamePartial : DbContext
    {
        public SearchNamePartial()
            : base("name=SearchNamePartial")
        {
        }

        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelImage> HotelImages { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomBed> RoomBeds { get; set; }
        public virtual DbSet<RoomImage> RoomImages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>()
                .Property(e => e.HotelEngName)
                .IsUnicode(false);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Longitude)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Latitude)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.Facilities)
                .WithRequired(e => e.Hotel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.HotelImages)
                .WithRequired(e => e.Hotel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.Rooms)
                .WithRequired(e => e.Hotel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.RoomPriceTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Room>()
                .Property(e => e.RoomPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Room)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.RoomBeds)
                .WithRequired(e => e.Room)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.RoomImages)
                .WithRequired(e => e.Room)
                .WillCascadeOnDelete(false);
        }
    }
}
