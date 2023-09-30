using BS_Adoga.Models.ViewModels.HotelDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.Account
{
    public class RePayViewModel
    {
        public string OrderID { get; set; }
        public string HotelID { get; set; }
        public string HotelName { get; set; }
        public int RoomQuantity { get; set; }
        public decimal RoomPriceTotal { get; set; }
        public bool PayStatus { get; set; }
    }
}