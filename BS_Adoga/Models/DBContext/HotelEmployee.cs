namespace BS_Adoga.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HotelEmployee")]
    public partial class HotelEmployee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HotelEmployee()
        {
            HotelEmpMappingHotels = new HashSet<HotelEmpMappingHotel>();
        }

        [StringLength(50)]
        public string HotelEmployeeID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string MD5HashPassword { get; set; }

        public string Logging { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime RegisterDatetime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HotelEmpMappingHotel> HotelEmpMappingHotels { get; set; }
    }
}
