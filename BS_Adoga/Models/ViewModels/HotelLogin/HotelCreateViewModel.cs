namespace BS_Adoga.Models.ViewModels.HotelLogin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class HotelCreateViewModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "�����s��")]
        public string HotelID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "�W��")]
        public string HotelName { get; set; }

        [StringLength(50)]
        [Display(Name = "�^��W��")]
        public string HotelEngName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "����")]
        public string HotelCity { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "�ϰ�")]
        public string HotelDistrict { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "�a�}")]
        public string HotelAddress { get; set; }

        [Required]
        [Display(Name = "���󶺩�")]
        public string HotelAbout { get; set; }

        [Display(Name = "�g��")]
        public decimal? Longitude { get; set; }

        [Display(Name = "�n��")]
        public decimal? Latitude { get; set; }

        [Required]
        [Display(Name = "�P��")]
        public int Star { get; set; }

        [Display(Name = "����")]
        public string Logging { get; set; }
    }
}
