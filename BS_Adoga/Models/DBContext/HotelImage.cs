namespace BS_Adoga.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HotelImage")]
    public partial class HotelImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ImageID { get; set; }

        [Required]
        [StringLength(50)]
        public string HotelID { get; set; }

        [Required]
        [StringLength(50)]
        public string ImageURL { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}
