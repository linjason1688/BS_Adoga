using BS_Adoga.Models.DBContext;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.homeViewModels
{
    public class demoshopViewModels
    {

        public IEnumerable<MyHotels> My_MyHotels { get; set; }
        public IEnumerable<MyHotels> My_MyCitys { get; set; }
        public IEnumerable<MyHotels> My_Mys { get; set; }
        public virtual IEnumerable<CardViewModels> My_CardViewModels { get; set; }
        public IPagedList<CardViewModels> PageOfHotelSearchVM { get; set; }
        public virtual IEnumerable<CardViewModels> My_CardViewModels2 { get; set; }
        public virtual IEnumerable<CardViewModels> My_city { get; set; }
      

        //public virtual IEnumerable<HotelImage> HotelImages { get; set; }
        public virtual IEnumerable<Card> Cards { get; set; }
        //public virtual IEnumerable<Room> Rooms { get; set; }
        //public virtual IEnumerable<RoomsDetail> RoomsDetails { get; set; }
    }
}