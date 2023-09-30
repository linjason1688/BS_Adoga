using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.Search;
using BS_Adoga.Repository;
namespace BS_Adoga.Service
{
    public class SearchCardService
    {
        private SearchCardRepository _r;
        public SearchCardService()
        {
            _r = new SearchCardRepository();
        }
        //public IQueryable<HotelSearchViewModel> GetHotels(string Name)
        //{
        //    var result = _r.GetHotel(Name);
        //    return result;
        //}

        public SearchCardViewModel GetSearchViewModelData(SearchDataViewModel info)
        {
            var data = new SearchCardViewModel
            {
                HotelSearchVM = _r.GetHotelAfterSearchByCityOrName(info),
                HotelOptionVM = _r.GetHotelOption(),
            };
            return data;
        }



        //API
        public IEnumerable<HotelSearchViewModel> GetHotelAfterSearchByCity(string city, string start, string end, int adult, int kid, int room)
        {
            var data = new SearchDataViewModel
            {
                HotelNameOrCity = city,
                CheckInDate = start,
                CheckOutDate = end,
                AdultCount = adult,
                KidCount = kid,
                RoomCount = room
            };

            var hotel = _r.GetHotelAfterSearchByCityOrName(data);
            return hotel;
        }

        //public IEnumerable<FacilityViewModel> getHotelFacility(string hotelID)
        //{

        //}

    }
}