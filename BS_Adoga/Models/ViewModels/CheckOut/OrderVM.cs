using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.ViewModels.HotelDetail;

namespace BS_Adoga.Models.ViewModels.CheckOut
{
    public class OrderVM
    {
        public CheckOutListViewModel checkOutListViewModel { get; set; }
        public RoomCheckOutData roomCheckOutViewModel { get; set; }
    }
}