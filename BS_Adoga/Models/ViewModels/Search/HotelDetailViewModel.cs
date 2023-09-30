using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.Search
{
    public class HotelDetailViewModel
    {
        public string HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelEngName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelCity { get; set; }
        public string HotelDistrict { get; set; }
        public int Star { get; set; }
    }
}