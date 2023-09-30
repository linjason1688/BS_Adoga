using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BS_Adoga.Repository;
using BS_Adoga.Service;
using BS_Adoga.Models.ViewModels.Search;

namespace BS_Adoga.APIController
{
    public class SearchApiController : ApiController
    {
        private SearchCardService _s;
        private SearchCardRepository _r;
        public SearchApiController()
        {
            _s = new SearchCardService();
            _r = new SearchCardRepository();
        }

        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetHotelByCity(string CityName, string startDate, string endDate, int adult, int kid, int room)
        {
            var allHotel = _s.GetHotelAfterSearchByCity(CityName, startDate, endDate, adult, kid, room);
            return Json(allHotel);
        }

        //[AcceptVerbs("GET", "POST")]
        //public IHttpActionResult GetFacilities(string hotelID)
        //{
        //    var allHotel = _s.getHotelFacility(hotelID);
        //    return Json(allHotel);
        //}
    }
}
