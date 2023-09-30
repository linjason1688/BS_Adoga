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

        [Display(Name = "房型編號")]
        [StringLength(50)]
        public string RoomID { get; set; }

        [Display(Name = "飯店編號")]
        [Required]
        [StringLength(50)]
        public string HotelID { get; set; }

        [Display(Name = "房型名稱")]
        [Required]
        [StringLength(50)]
        public string RoomName { get; set; }

        [Display(Name = "人數")]
        public int NumberOfPeople { get; set; }

        [Display(Name = "房間數")]
        public int RoomCount { get; set; }

        [Display(Name = "房價")]
        [Column(TypeName = "money")]
        public decimal RoomPrice { get; set; }

        [Display(Name = "浴室型態")]
        public int TypesOfBathroomID { get; set; }

        [Display(Name = "是否禁菸")]
        public bool NoSmoking { get; set; }

        [Display(Name = "是否早餐")]
        public bool Breakfast { get; set; }

        [Display(Name = "是否WIFI")]
        public bool WiFi { get; set; }

        [Display(Name = "是否電視")]
        public bool TV { get; set; }

        [Display(Name = "紀錄")]
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
