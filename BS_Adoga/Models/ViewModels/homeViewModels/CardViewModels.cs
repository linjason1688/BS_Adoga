using BS_Adoga.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.homeViewModels
{
    public class CardViewModels
    {
        public string HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelCity { get; set; }
        public string HotelAddress  { get; set; }
        public string HotelEngName { get; set; }
        public int Star { get; set; }
        public string ImageID { get; set; }
     
        public virtual IEnumerable<Hotel> Hotels { get; set; }
        public virtual IEnumerable<Card> Cards { get; set; }

        public MyHoteiImages My_HotelImages { get; set; }
        public string ImageURL { get; set; }
        
        public MyRoom My_Rooms { get; set; }
        public MyRoomsDetails My_RoomsDetails { get; set; }
        public MyMessageBoard<Decimal> My_MessageBoard { get; set; }
        public decimal ddd { get; set; }

    }
}