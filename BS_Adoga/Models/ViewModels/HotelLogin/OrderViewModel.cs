namespace BS_Adoga.Models.ViewModels.HotelLogin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public class OrderViewModel
    {
        public string OrderID { get; set; }

        public string CustomerID { get; set; }

        public string RoomID { get; set; }

        public string RoomName { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int RoomCount { get; set; }

        public decimal RoomPriceTotal { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Country { get; set; }

        public string SmokingPreference { get; set; }

        public string BedPreference { get; set; }

        public string ArrivingTime { get; set; }

        public bool PaymentStatus { get; set; }
    }
}