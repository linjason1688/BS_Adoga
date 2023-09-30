using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.HotelDetail
{
    public class RoomTypeVM
    {
        public string HotelID { get; set; }

        public string RoomID { get; set; }

        public string RoomName { get; set; }

        public string RoomImgURL { get; set; }

        public bool NoSmoking { get; set; }

        public bool Breakfast { get; set; }

        public bool WiFi { get; set; }

        public string BathroomName { get; set; }

        public IQueryable<RoomBedVM> RoomBed { get; set; }

        public int Adult { get; set; }

        public int Child { get; set; }

        public int RoomOrder { get; set; }

        public decimal RoomPrice { get; set; }

        public decimal RoomDiscount { get; set; }

        public decimal RoomNowPrice { get; set; }

        public int RoomLeft { get; set; }
    }
}