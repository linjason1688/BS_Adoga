namespace BS_Adoga.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RoomImage")]
    public partial class RoomImage
    {
        [Key]
        [StringLength(55)]
        public string ImageID { get; set; }

        [Required]
        [StringLength(50)]
        public string HotelID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomID { get; set; }

        [Required]
        public string ImageURL { get; set; }

        public virtual Room Room { get; set; }
    }
}
