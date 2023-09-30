namespace BS_Adoga.Models.DBContext
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

        [Display(Name = "�Ы��s��")]
        [StringLength(50)]
        public string RoomID { get; set; }

        [Display(Name = "�����s��")]
        [Required]
        [StringLength(50)]
        public string HotelID { get; set; }

        [Display(Name = "�Ы��W��")]
        [Required]
        [StringLength(50)]
        public string RoomName { get; set; }

        [Display(Name = "�H��")]
        public int NumberOfPeople { get; set; }

        [Display(Name = "�ж���")]
        public int RoomCount { get; set; }

        [Display(Name = "�л�")]
        [Column(TypeName = "money")]
        public decimal RoomPrice { get; set; }

        [Display(Name = "�D�ǫ��A")]
        public int TypesOfBathroomID { get; set; }

        [Display(Name = "�O�_�T��")]
        public bool NoSmoking { get; set; }

        [Display(Name = "�O�_���\")]
        public bool Breakfast { get; set; }

        [Display(Name = "�O�_WIFI")]
        public bool WiFi { get; set; }

        [Display(Name = "�O�_�q��")]
        public bool TV { get; set; }

        [Display(Name = "����")]
        [Required]
        public string Logging { get; set; }

        public virtual BathroomType BathroomType { get; set; }

        public virtual Hotel Hotel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomBed> RoomBeds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomImage> RoomImages { get; set; }

        public virtual RoomsDetail RoomsDetail { get; set; }
    }
}
