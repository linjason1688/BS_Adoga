using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BS_Adoga.Models.DBContext
{
    public partial class AdogaContext : DbContext
    {
        public AdogaContext()
            : base("name=AdogaContext")
        {
        }

        public virtual DbSet<BathroomType> BathroomTypes { get; set; }
        public virtual DbSet<BedType> BedTypes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelEmployee> HotelEmployees { get; set; }
        public virtual DbSet<HotelEmpMappingHotel> HotelEmpMappingHotels { get; set; }
        public virtual DbSet<HotelImage> HotelImages { get; set; }
        public virtual DbSet<HotelImageUpload> HotelImageUploads { get; set; }
        public virtual DbSet<MessageBoard> MessageBoards { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomBed> RoomBeds { get; set; }
        public virtual DbSet<RoomImage> RoomImages { get; set; }
        public virtual DbSet<RoomsDetail> RoomsDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BathroomType>()
                .HasMany(e => e.Rooms)
                .WithRequired(e => e.BathroomType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BedType>()
                .HasMany(e => e.RoomBeds)
                .WithRequired(e => e.BedType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

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
                .HasMany(e => e.HotelEmpMappingHotels)
                .WithRequired(e => e.Hotel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.HotelImages)
                .WithRequired(e => e.Hotel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.HotelImageUploads)
                .WithRequired(e => e.Hotel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.Rooms)
                .WithRequired(e => e.Hotel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HotelEmployee>()
                .HasMany(e => e.HotelEmpMappingHotels)
                .WithRequired(e => e.HotelEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.RoomPriceTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()
                .HasOptional(e => e.MessageBoard)
                .WithRequired(e => e.Order);

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

            modelBuilder.Entity<Room>()
                .HasOptional(e => e.RoomsDetail)
                .WithRequired(e => e.Room);

            modelBuilder.Entity<RoomsDetail>()
                .Property(e => e.RoomDiscount)
                .HasPrecision(19, 4);
        }
    }
}
