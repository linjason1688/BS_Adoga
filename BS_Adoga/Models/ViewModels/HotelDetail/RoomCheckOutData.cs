using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.HotelDetail
{
    public class RoomCheckOutData
    {
        public string HotelID { get; set; }

        public string HotelFullName { get; set; }

        public string HotelImageUrl { get; set; }

        public string Address { get; set; }

        public int Star { get; set; }        

        public string RoomID { get; set; }

        public string RoomName { get; set; }

        public bool NoSmoking { get; set; }

        public bool Breakfast { get; set; }

        public string BedType { get; set; }
        //public IQueryable<RoomBedVM> RoomBed { get; set; }
        public string CheckInDate { get; set; }

        public string CheckOutDate { get; set; }

        public int CountNight { get; set; }

        public int RoomOrder { get; set; }

        public int Adult { get; set; }

        public int Child { get; set; }

        public decimal RoomPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalPrice { get; set; }

    }
}