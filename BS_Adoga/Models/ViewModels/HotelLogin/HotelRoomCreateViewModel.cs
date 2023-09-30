using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.HotelLogin
{
    public class HotelRoomCreateViewModel
    {
        [Display(Name = "飯店編號")]
        [Required]
        [StringLength(50)]
        public string HotelID { get; set; }

        [Display(Name = "房型名稱")]
        [Required]
        [StringLength(50)]
        public string RoomName { get; set; }

        [Display(Name = "人數")]
        public int NumberOfPeople { get; set; }

        [Display(Name = "房間數")]
        public int RoomCount { get; set; }

        [Display(Name = "房價")]
        [Column(TypeName = "money")]
        public decimal RoomPrice { get; set; }

        [Display(Name = "浴室型態編號")]
        public int TypesOfBathroomID { get; set; }

        [Display(Name = "是否禁菸")]
        public bool NoSmoking { get; set; }

        [Display(Name = "是否早餐")]
        public bool Breakfast { get; set; }

        [Display(Name = "是否WIFI")]
        public bool WiFi { get; set; }

        [Display(Name = "是否電視")]
        public bool TV { get; set; }
    }
}