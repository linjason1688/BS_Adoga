using BS_Adoga.Models.ViewModels.HotelDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.Account
{
    public class MemberBookingViewModel
    {
        public string OrderID { get; set; }
        public string HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelEngName { get; set; }
        public string HotelImageURL { get; set; }
        public IQueryable<RoomBedVM> RoomBed { get; set; }
        public decimal RoomPriceTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string City { get; set; }
        public bool Breakfast { get; set; }
        public bool PayStatus { get; set; }
    }
}