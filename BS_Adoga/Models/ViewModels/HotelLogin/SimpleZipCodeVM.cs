using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.HotelLogin
{
    public class SimpleZipCodeVM
    {
        public string city { get; set; }
        public List<districts> districts { get;set;}
    }
}