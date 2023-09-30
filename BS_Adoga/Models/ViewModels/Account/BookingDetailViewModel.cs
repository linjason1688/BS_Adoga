using BS_Adoga.Models.ViewModels.HotelDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.Account
{
    public class BookingDetailViewModel
    {
        public string HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelEngName { get; set; }
        public string HotelImageURL { get; set; }
        public int Star { get; set; }
        public string HotelCity { get; set; }
        public string HotelDistrict { get; set; }
        public string HotelAddress { get; set; }
        public string OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string RoomName { get; set; }
        public int RoomCount { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public IQueryable<RoomBedVM> RoomBed { get; set; }
        public decimal RoomPriceTotal { get; set; }
        public bool PayStatus { get; set; }
    }
}