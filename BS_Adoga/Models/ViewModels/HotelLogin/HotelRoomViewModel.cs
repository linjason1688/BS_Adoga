using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.HotelLogin
{
    public class HotelRoomViewModel
    {
        [Display(Name = "飯店編號")]
        public string HotelID { get; set; }

        [Display(Name = "飯店名稱")]
        public string HotelName { get; set; }

        [Display(Name = "房型數量")]
        public int RoomCount { get; set; }
    }
}