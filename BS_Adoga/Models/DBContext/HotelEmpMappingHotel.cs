namespace BS_Adoga.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HotelEmpMappingHotel
    {
        [Key]
        [StringLength(80)]
        public string MappingID { get; set; }

        [Required]
        [StringLength(50)]
        public string HotelID { get; set; }

        [Required]
        [StringLength(50)]
        public string HotelEmployeeID { get; set; }

        public virtual Hotel Hotel { get; set; }

        public virtual HotelEmployee HotelEmployee { get; set; }
    }
}