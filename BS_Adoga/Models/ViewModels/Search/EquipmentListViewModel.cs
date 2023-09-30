using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.ViewModels.Search;
using BS_Adoga.Models.ViewModels.HotelDetail;

namespace BS_Adoga.Models.ViewModels.Search
{
    public class EquipmentListViewModel
    {
        public IEnumerable<FacilityViewModel> FacilityVM { get; set; }

        public RoomBedVM BedType { get; set; }
    }
}