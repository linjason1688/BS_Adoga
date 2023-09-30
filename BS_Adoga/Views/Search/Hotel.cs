namespace BS_Adoga.Views.Search
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hotel")]
    public partial class Hotel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hotel()
        {
            Facilities = new HashSet<Facility>();
            HotelImages = new HashSet<HotelImage>();
            Rooms = new HashSet<Room>();
        }

        [StringLength(50)]
        public string HotelID { get; set; }

        [Required]
        [StringLength(50)]
        public string HotelName { get; set; }

        [StringLength(50)]
        public string HotelEngName { get; set; }

        [Required]
        [StringLength(50)]
        public string HotelCity { get; set; }

        [Required]
        [StringLength(50)]
        public string HotelDistrict { get; set; }

        [Required]
        [StringLength(100)]
        public string HotelAddress { get; set; }

        [Required]
        public string HotelAbout { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }

        public int Star { get; set; }

        [Required]
        public string Logging { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Facility> Facilities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HotelImage> HotelImages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
