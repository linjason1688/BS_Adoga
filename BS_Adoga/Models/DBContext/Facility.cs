namespace BS_Adoga.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Facility
    {
        [Display(Name = "設施ID")]
        [Key]
        public int FacilitieID { get; set; }

        [Display(Name = "飯店ID")]
        [Required]
        [StringLength(50)]
        public string HotelID { get; set; }

        [Display(Name = "游泳池")]
        public bool SwimmingPool { get; set; }

        [Display(Name = "機場接送")]
        public bool AirportTransfer { get; set; }

        [Display(Name = "親子友善住宿")]
        public bool FamilyChildFriendly { get; set; }

        [Display(Name = "餐廳")]
        public bool Restaurants { get; set; }

        [Display(Name = "夜店")]
        public bool Nightclub { get; set; }

        [Display(Name = "高爾夫球場")]
        public bool GolfCourse { get; set; }

        [Display(Name = "網路")]
        public bool Internet { get; set; }

        [Display(Name = "健身房")]
        public bool Gym { get; set; }

        [Display(Name = "禁菸")]
        public bool NoSmoking { get; set; }

        [Display(Name = "吸菸區")]
        public bool SmokingArea { get; set; }

        [Display(Name = "無障礙友善設施")]
        public bool FacilitiesFordisabledGuests { get; set; }

        [Display(Name = "停車場")]
        public bool CarPark { get; set; }

        [Display(Name = "24小時櫃台服務")]
        public bool FrontDesk { get; set; }

        [Display(Name = "Spa桑拿")]
        public bool SpaSauna { get; set; }

        [Display(Name = "可帶寵物")]
        public bool PetsAllowed { get; set; }

        [Display(Name = "商務設施")]
        public bool BusinessFacilities { get; set; }

        [Display(Name = "紀錄")]
        [Required]
        public string Logging { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}
