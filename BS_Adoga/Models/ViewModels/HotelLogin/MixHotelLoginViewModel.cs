using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.HotelLogin
{
    public class MixHotelLoginViewModel
    {
        public virtual HotelLoginViewModel HotelLoginView { get; set; }
        public virtual RegisterViewModel RegisterViewModel { get; set; }
    }
}