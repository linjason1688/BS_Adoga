namespace BS_Adoga.Models.DBContext
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
            HotelEmpMappingHotels = new HashSet<HotelEmpMappingHotel>();
            HotelImages = new HashSet<HotelImage>();
            HotelImageUploads = new HashSet<HotelImageUpload>();
            Rooms = new HashSet<Room>();
        }

        [Display(Name = "¶º©±½s¸¹")]
        [StringLength(50)]
        public string HotelID { get; set; }

        [Display(Name = "¶º©±¦WºÙ")]
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
        public virtual ICollection<HotelEmpMappingHotel> HotelEmpMappingHotels { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HotelImage> HotelImages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HotelImageUpload> HotelImageUploads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
