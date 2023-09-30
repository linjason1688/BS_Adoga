namespace BS_Adoga.Views.Search
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RoomBed")]
    public partial class RoomBed
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string RoomID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypesOfBedsID { get; set; }

        public int Amount { get; set; }

        public virtual Room Room { get; set; }
    }
}
