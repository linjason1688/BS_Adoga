using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.Search
{
    public class HotelSearchViewModel
    {
        public string HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelEngName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelCity { get; set; }
        public string HotelDistrict { get; set; }
        public int Star { get; set; }
        public string imgUrl { get; set; }
        public RoomViewModel I_RoomVM { get; set; }
        public RoomDetailViewModel I_RoomDetailVM { get; set; }
        public FacilityViewModel I_FacilityVM { get; set; }
    }
}