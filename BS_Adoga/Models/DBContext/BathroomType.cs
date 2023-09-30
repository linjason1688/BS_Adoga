namespace BS_Adoga.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BathroomType")]
    public partial class BathroomType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BathroomType()
        {
            Rooms = new HashSet<Room>();
        }

        [Display(Name = "�D�ǫ��A")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypesOfBathroomID { get; set; }

        [Display(Name = "�D�ǫ��A")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
