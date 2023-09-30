namespace BS_Adoga.Views.Search
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Facility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FacilitieID { get; set; }

        [Required]
        [StringLength(50)]
        public string HotelID { get; set; }

        public bool SwimmingPool { get; set; }

        public bool AirportTransfer { get; set; }

        public bool FamilyChildFriendly { get; set; }

        public bool Restaurants { get; set; }

        public bool Nightclub { get; set; }

        public bool GolfCourse { get; set; }

        public bool Internet { get; set; }

        public bool Gym { get; set; }

        public bool NoSmoking { get; set; }

        public bool SmokingArea { get; set; }

        public bool FacilitiesFordisabledGuests { get; set; }

        public bool CarPark { get; set; }

        public bool FrontDesk { get; set; }

        public bool SpaSauna { get; set; }

        public bool PetsAllowed { get; set; }

        public bool BusinessFacilities { get; set; }

        [Required]
        public string Logging { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}
