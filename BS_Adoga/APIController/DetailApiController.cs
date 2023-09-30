using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;
using BS_Adoga.Service;
using BS_Adoga.Repository;
using BS_Adoga.Models.ViewModels.HotelDetail;
using System.Web.Http.Description;
using Newtonsoft.Json;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net.Http;
using System.Net;
using System.Web.Mvc;

namespace BS_Adoga.APIController.HotelDetail
{
    public class DetailApiController : ApiController
    {
        private HotelDetailService _service;
        private HotelDetailRepository _repository;

        public DetailApiController()
        {
            _service = new HotelDetailService();
            _repository = new HotelDetailRepository();
        }

        // GET: DetailApi
        //[HttpGet]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllRoom(string hotelName, string startDate, string endDate, int orderRoom, int adult, int child)
        {
            string hotelId = _repository.GetHotelIdByName(hotelName);
            var data = _service.GetRoomTypeByFilter(hotelId, startDate, endDate, orderRoom, adult, child);
            return Json(data);
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetSpecificRoom(string hotelName, string startDate, string endDate, int orderRoom, int adult, int child, bool freeBreakfast, bool noSmoking, bool family)
        {
            string hotelId = _repository.GetHotelIdByName(hotelName);
            var data = _service.GetSpecificRoomType(hotelId, startDate, endDate, orderRoom, adult, child, freeBreakfast, noSmoking, family);

            return Json(data);
            //return Ok(JsonConvert.SerializeObject(a));
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public IHttpActionResult GetHotelFacilities(string hotelName = "台中商旅")
        {
            string hotelId = _repository.GetHotelIdByName(hotelName);
            var data = _service.GetHotelFacilityById(hotelId);
            return Json(data);
            //return Ok(JsonConvert.SerializeObject(a));
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public IHttpActionResult GetRoomImages(string hotelID,string roomID)
        {
            var data = _service.GetRoomImagesById(hotelID, roomID);
            return Json(data);
            //return Ok(JsonConvert.SerializeObject(a));
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public IHttpActionResult GetHotelMessage(string hotelID = "hotel04")
        {
            var data = _service.GetHotelMessageById(hotelID);
            return Json(data);
        }



    }
}