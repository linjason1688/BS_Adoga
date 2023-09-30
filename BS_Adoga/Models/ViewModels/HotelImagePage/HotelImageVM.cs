using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.HotelImagePage
{
    public class HotelImageVM
    {
        public string HotelID { get; set; }

        public string HotelEmployeeID { get; set; }

        public IEnumerable<HotelOption> HotelOptions { get; set; }       

        public IEnumerable<ImagesVM> Images { get; set; }

    }
}