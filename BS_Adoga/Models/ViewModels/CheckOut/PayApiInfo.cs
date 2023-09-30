using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.CheckOut
{
    public class PayApiInfo
    {
        public string OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public string HotelName { get; set; }
        public int RoomQuantity { get; set; }
    }
}