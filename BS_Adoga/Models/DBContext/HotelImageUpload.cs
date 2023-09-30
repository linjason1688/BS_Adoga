namespace BS_Adoga.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HotelImageUpload")]
    public partial class HotelImageUpload
    {
        [Key]
        [StringLength(60)]
        public string ImageID { get; set; }

        [Required]
        [StringLength(50)]
        public string HotelID { get; set; }

        [Required]
        public string ImageURL { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}