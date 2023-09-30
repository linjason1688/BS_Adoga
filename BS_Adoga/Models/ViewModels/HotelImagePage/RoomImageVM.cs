using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.HotelImagePage
{
    public class RoomImageVM
    {
        public string HotelEmployeeID { get; set; }

        public IEnumerable<HotelOption> HotelOptions { get; set; }
    }
}