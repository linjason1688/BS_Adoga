using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.Search
{
    public class RoomViewModel
    {
        public string RoomID { get; set; }
        public string HotelID { get; set; }
        public decimal RoomPrice { get; set; }
        public bool NoSmoking { get; set;}
        public bool Breakfast { get; set;}
        public bool WiFi { get; set;}
        //public bool TV { get; set;}
    }
}