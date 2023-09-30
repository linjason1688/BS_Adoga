using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.Search
{
    public class RoomDetailViewModel
    {
        public string RoomID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int RoomCount { get; set; }
        public int RoomOrder { get; set; }
        public decimal RoomDiscount { get; set; }
        //public bool OpenRoom { get; set; }
    }
}