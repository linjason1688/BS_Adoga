using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.Search
{
    public class SearchDataViewModel
    {
        public string HotelNameOrCity { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public int RoomCount { get; set; }
        public int AdultCount { get; set; }
        public int KidCount { get; set; }
    }
}