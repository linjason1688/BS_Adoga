namespace BS_Adoga.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [StringLength(50)]
        public string OrderID { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime OrderDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CheckInDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CheckOutDate { get; set; }

        public int RoomCount { get; set; }

        [Column(TypeName = "money")]
        public decimal RoomPriceTotal { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        [StringLength(50)]
        public string SmokingPreference { get; set; }

        [Required]
        [StringLength(50)]
        public string BedPreference { get; set; }

        [Required]
        [StringLength(50)]
        public string ArrivingTime { get; set; }

        public bool PaymentStatus { get; set; }

        [Required]
        public string Logging { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual MessageBoard MessageBoard { get; set; }

        public virtual Room Room { get; set; }
    }
}
