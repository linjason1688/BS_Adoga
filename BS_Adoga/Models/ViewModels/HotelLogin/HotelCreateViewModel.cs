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
        [Display(Name = "飯店編號")]
        public string HotelID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "名稱")]
        public string HotelName { get; set; }

        [StringLength(50)]
        [Display(Name = "英文名稱")]
        public string HotelEngName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "縣市")]
        public string HotelCity { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "區域")]
        public string HotelDistrict { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "地址")]
        public string HotelAddress { get; set; }

        [Required]
        [Display(Name = "關於飯店")]
        public string HotelAbout { get; set; }

        [Display(Name = "經度")]
        public decimal? Longitude { get; set; }

        [Display(Name = "緯度")]
        public decimal? Latitude { get; set; }

        [Required]
        [Display(Name = "星級")]
        public int Star { get; set; }

        [Display(Name = "紀錄")]
        public string Logging { get; set; }
    }
}
