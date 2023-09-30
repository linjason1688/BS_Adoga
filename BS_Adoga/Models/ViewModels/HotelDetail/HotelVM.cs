using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.HotelDetail
{
    public class HotelVM
    {
        public string HotelID { get; set; }

        public string HotelName { get; set; }

        public string HotelEngName { get; set; }

        public string HotelCity { get; set; }

        public string HotelAddress { get; set; }

        public string HotelAbout { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }

        public int Star { get; set; }
    }
}