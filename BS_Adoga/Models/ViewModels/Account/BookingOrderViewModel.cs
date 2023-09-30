using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.ViewModels.HotelDetail;

namespace BS_Adoga.Models.ViewModels.Account
{
    public class BookingOrderViewModel
    {
        public string CustomerID { get; set; }
        public string OrderID { get; set; }
        public string HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelEngName { get; set; }
        public IQueryable<RoomBedVM> RoomBed { get; set; }
        public decimal RoomPriceTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int FewDaysAgo { get; set; }
        public string OrderDateStr { get; set; }
        public string CheckInDay { get; set; }
        public string CheckInWeek { get; set; }
        public string CheckInMonth { get; set; }
        public string CheckOutDay { get; set; }
        public string CheckOutWeek { get; set; }
        public string CheckOutMonth { get; set; }
        public int CheckCheckOut { get; set; }
        public string City { get; set; }
        public bool Breakfast { get; set; }
        public bool PayStatus { get; set; }
        public bool In24Hours { get; set; }
        public string HotelImageURL { get; set; }
    }
}