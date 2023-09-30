namespace BS_Adoga.Views.Search
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Room")]
    public partial class Room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            Orders = new HashSet<Order>();
            RoomBeds = new HashSet<RoomBed>();
            RoomImages = new HashSet<RoomImage>();
        }

        [StringLength(50)]
        public string RoomID { get; set; }

        [Required]
        [StringLength(50)]
        public string HotelID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomName { get; set; }

        public int NumberOfPeople { get; set; }

        public int RoomCount { get; set; }

        [Column(TypeName = "money")]
        public decimal RoomPrice { get; set; }

        public int TypesOfBathroomID { get; set; }

        public bool NoSmoking { get; set; }

        public bool Breakfast { get; set; }

        public bool WiFi { get; set; }

        public bool TV { get; set; }

        [Required]
        public string Logging { get; set; }

        public virtual Hotel Hotel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomBed> RoomBeds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomImage> RoomImages { get; set; }
    }
}
