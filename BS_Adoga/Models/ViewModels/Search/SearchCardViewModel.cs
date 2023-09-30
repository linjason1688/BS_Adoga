using BS_Adoga.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace BS_Adoga.Models.ViewModels.Search
{
    public class SearchCardViewModel
    {
        public IEnumerable<HotelSearchViewModel> HotelSearchVM { get; set; }//用來接收找出來的全部資料的VM
        //public IPagedList<HotelSearchViewModel> PageOfHotelSearchVM { get; set; }//排序+分頁後的HotelSearchVM

        
        public IEnumerable<HotelOptionViewModel>  HotelOptionVM { get; set; }
        //public EquipmentListViewModel EquipmentVM { get; set; }

    }
}