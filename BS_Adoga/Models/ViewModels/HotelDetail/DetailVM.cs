using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.ViewModels.Search;
using BS_Adoga.Models.ViewModels.HotelImagePage;

namespace BS_Adoga.Models.ViewModels.HotelDetail
{
    public class DetailVM
    {
        public HotelVM hotelVM { get; set; }

        //public IQueryable<RoomTypeVM> roomTypeVM { get; set; }
        public IEnumerable<RoomTypeVM> roomTypeVM { get; set; }

        public IEnumerable<HotelOptionViewModel> hotelOptionVM { get; set; }

        public string[] HotelImages { get; set; }

        public ScoreVM HotelScoreVM { get; set; }
    }
}