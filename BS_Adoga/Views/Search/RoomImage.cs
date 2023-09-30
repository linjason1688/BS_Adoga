namespace BS_Adoga.Views.Search
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ImageID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomID { get; set; }

        [Required]
        [StringLength(50)]
        public string ImageURL { get; set; }

        public virtual Room Room { get; set; }
    }
}
