namespace BS_Adoga.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Facility
    {
        [Display(Name = "�]�IID")]
        [Key]
        public int FacilitieID { get; set; }

        [Display(Name = "����ID")]
        [Required]
        [StringLength(50)]
        public string HotelID { get; set; }

        [Display(Name = "��a��")]
        public bool SwimmingPool { get; set; }

        [Display(Name = "�������e")]
        public bool AirportTransfer { get; set; }

        [Display(Name = "�ˤl�͵���J")]
        public bool FamilyChildFriendly { get; set; }

        [Display(Name = "�\�U")]
        public bool Restaurants { get; set; }

        [Display(Name = "�]��")]
        public bool Nightclub { get; set; }

        [Display(Name = "�����Ҳy��")]
        public bool GolfCourse { get; set; }

        [Display(Name = "����")]
        public bool Internet { get; set; }

        [Display(Name = "������")]
        public bool Gym { get; set; }

        [Display(Name = "�T��")]
        public bool NoSmoking { get; set; }

        [Display(Name = "�l�Ұ�")]
        public bool SmokingArea { get; set; }

        [Display(Name = "�L��ê�͵��]�I")]
        public bool FacilitiesFordisabledGuests { get; set; }

        [Display(Name = "������")]
        public bool CarPark { get; set; }

        [Display(Name = "24�p���d�x�A��")]
        public bool FrontDesk { get; set; }

        [Display(Name = "Spa�᮳")]
        public bool SpaSauna { get; set; }

        [Display(Name = "�i�a�d��")]
        public bool PetsAllowed { get; set; }

        [Display(Name = "�Ӱȳ]�I")]
        public bool BusinessFacilities { get; set; }

        [Display(Name = "����")]
        [Required]
        public string Logging { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}
