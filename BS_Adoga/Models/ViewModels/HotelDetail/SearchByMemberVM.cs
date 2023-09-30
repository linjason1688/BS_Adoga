using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.HotelDetail
{
    public class SearchByMemberVM
    {
        public string CityOrHotel { get; set; }

        public string CheckInDate { get; set; }

        public string CheckOutDate { get; set; }

        public int CountNight { get; set; }

        public int RoomOrder { get; set; }

        //public string TravelType { get; set; }

        public int Adult { get; set; }

        public int Child { get; set; }
    }
}