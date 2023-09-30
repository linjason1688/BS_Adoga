namespace BS_Adoga.Models.ViewModels.HotelLogin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class HotelListViewModel
    {
        [Display(Name = "飯店編號")]
        public string HotelID { get; set; }

        [Display(Name = "飯店名稱")]
        public string HotelName { get; set; }

        [Display(Name = "英文名稱")]
        public string HotelEngName { get; set; }

        [Display(Name = "縣市")]
        public string HotelCity { get; set; }

        [Display(Name = "區域")]
        public string HotelDistrict { get; set; }

        [Display(Name = "地址")]
        public string HotelAddress { get; set; }

    }
}
