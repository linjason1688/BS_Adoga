namespace BS_Adoga.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RoomsDetail")]
    public partial class RoomsDetail
    {
        [Key]
        [StringLength(50)]
        public string RDID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CheckInDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CheckOutDate { get; set; }

        public int RoomCount { get; set; }

        public int RoomOrder { get; set; }

        [Column(TypeName = "money")]
        public decimal RoomDiscount { get; set; }

        public bool OpenRoom { get; set; }

        [Required]
        public string Logging { get; set; }

        public virtual Room Room { get; set; }
    }
}
