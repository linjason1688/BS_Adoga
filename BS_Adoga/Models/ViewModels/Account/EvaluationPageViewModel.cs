using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.Account
{
    public class EvaluationPageViewModel
    {
        public string OrderID { get; set; }

        public string HotelID { get; set; }

        public string CustomerID { get; set; }

        public string Title { get; set; }

        public string MessageDate { get; set; }

        public string MessageText { get; set; }

        public string Score { get; set; }

        public string CustomerName { get; set; }

        public string HotelName { get; set; }

        public string RoomName { get; set; }

        public string Stay { get; set; }
    }
}