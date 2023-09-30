using BS_Adoga.Models.ViewModels.HotelDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.CheckOut
{
    public class ECPayResultsViewModel
    {
        public string OrderId { get; set; }
        public string TradeDate { get; set; }
        public string PaymentDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,0}")]
        public string TradePrice { get; set; }
        public string PaymentType { get; set; }
    }
}