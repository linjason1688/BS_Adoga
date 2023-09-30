using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.ViewModels.HotelImagePage;


namespace BS_Adoga.Models.ViewModels.HotelDetail
{
    public class Album_RoomImagesVM
    {
        public string RoomID { get; set; }

        public string RoomName { get; set; }

        public string Score { get; set; }

        public bool NoSmoking { get; set; }

        public bool Breakfast { get; set; }

        public bool WiFi { get; set; }

        public string BathroomName { get; set; }

        public IQueryable<RoomBedVM> RoomBed { get; set; }

        public decimal RoomDiscount { get; set; }

        public decimal RoomNowPrice { get; set; }

        public int RoomLeft { get; set; }

        public IEnumerable<ImagesVM> Images { get; set; }
    }
}