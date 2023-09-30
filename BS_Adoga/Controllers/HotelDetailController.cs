using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelDetail;
using BS_Adoga.Models.ViewModels.CheckOut;
using BS_Adoga.Models.ViewModels.Search;
using BS_Adoga.Service;
using BS_Adoga.Repository;
using System.Net;

namespace BS_Adoga.Controllers
{
    public class HotelDetailController : Controller
    {
        //private AdogaContext _context;
        private HotelDetailService _service;
        private HotelDetailRepository _repository;
        public HotelDetailController()
        {
            //_context = new AdogaContext();
            _service = new HotelDetailService();
            _repository = new HotelDetailRepository();
        }

        // GET: HotelDetail
        public ActionResult HotelDetail(string hotelName, string startDate, string endDate, int orderRoom, int adult, int child)
        {
            if (hotelName == null || startDate == null || endDate ==null || orderRoom == 0 || adult == 0 )
            {
                return Content("請在搜尋框選好全部欄位的資料，才可幫您進行飯店查詢喔。");
            }

            //var a = DateTime.Parse(startDate);
            DateTime checkInDate = DateTime.Parse(startDate);
            DateTime checkOutDate = DateTime.Parse(endDate);
            SearchByMemberVM searchData = new SearchByMemberVM()
            {
                CityOrHotel = hotelName,
                CheckInDate = startDate,
                CheckOutDate = endDate,
                RoomOrder = orderRoom,
                CountNight = new TimeSpan(checkOutDate.Ticks - checkInDate.Ticks).Days,
                Adult = adult,
                Child = child
            };
            TempData["SearchData"] = searchData;            

            DetailVM hotelDetail;
            string hotelId = _repository.GetHotelIdByName(hotelName);
            
            if (hotelId != null)
                hotelDetail = _service.GetDetailVM(hotelId, startDate, endDate, orderRoom, adult, child);
            else if (TempData["search"] != null)
                hotelDetail = _service.GetDetailVM(hotelId, startDate, endDate, orderRoom, adult, child);
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //hotelDetail = _service.GetDetailVM("hotel04"); //應該做報錯

            return View(hotelDetail);
        }

        public ActionResult GetTempData(string search, string date_range, string people, string room)
        {
            string[] human = people.Split(',');
            string[] adultSplit = human[0].Split('位');
            string[] kidSplit =new string[1];
            int adult = int.Parse(adultSplit[0]);
            int kid = 0;
            if (human.Length > 1)
            {
                kidSplit = human[1].Split('位');
                kid = int.Parse(kidSplit[0]);
            }

            string[] roomSplit = room.Split('間');

            string[] date = date_range.Split('-');
            string start = DateTime.Parse(date[0]).ToString("yyyy-MM-dd");
            string end = DateTime.Parse(date[1]).ToString("yyyy-MM-dd");

            string hotelId = _repository.GetHotelIdByName(search);
            if (hotelId == null)
            {
                //如果沒有找到id，表示是搜尋城市，跳轉至search。
                SearchDataViewModel info = new SearchDataViewModel()
                {
                    HotelNameOrCity = search,
                    CheckInDate = start,
                    CheckOutDate = end,
                    RoomCount = int.Parse(roomSplit[0]),
                    AdultCount = adult,
                    KidCount = kid
                };
                return RedirectToAction("Search", "Search",info);
            }
            else
            {
                return RedirectToAction("HotelDetail", new
                {
                    hotelName = search,
                    startDate = start,
                    endDate = end,
                    orderRoom = int.Parse(roomSplit[0]),
                    adult = adult,
                    child = kid
                });
            }            
        }

        
        [HttpGet]
        public ActionResult SetCheckOutData( string hotelId, string roomId,string roomName,bool noSmoking,bool breakfast,string bedType,
                                                int roomOrder,decimal roomPrice,decimal roomDiscount, decimal roomNowPrice)
        {
            var hotel = _service.GetHotelById(hotelId);
            var searchData = (SearchByMemberVM)TempData["SearchData"];
            TempData.Keep("SearchData");

            //DateTime checkInDate = DateTime.Parse(searchData.CheckInDate);
            //DateTime checkOutDate = DateTime.Parse(searchData.CheckOutDate);
            //int adult = searchData.Adult;
            //int child = searchData.Child;

            OrderVM orderData = new OrderVM()
            {
                roomCheckOutViewModel = new RoomCheckOutData
                {
                    HotelID = hotel.HotelID,
                    HotelFullName = hotel.HotelName + " (" + hotel.HotelEngName + ")",
                    HotelImageUrl = _repository.GetFirstHotelImageById(hotelId),
                    Address = hotel.HotelAddress,
                    Star = hotel.Star,
                    RoomID = roomId,
                    RoomName = roomName,
                    NoSmoking = noSmoking,
                    Breakfast = breakfast,
                    BedType = bedType,
                    CheckInDate = searchData.CheckInDate,
                    CheckOutDate = searchData.CheckOutDate,
                    Adult = searchData.Adult,
                    Child = searchData.Child,
                    CountNight = searchData.CountNight,
                    RoomOrder = roomOrder,
                    RoomPrice = roomPrice,
                    Discount = roomDiscount,
                    TotalPrice = roomNowPrice
                }
            };

            orderData.roomCheckOutViewModel.TotalPrice = orderData.roomCheckOutViewModel.RoomPrice * orderData.roomCheckOutViewModel.CountNight * orderData.roomCheckOutViewModel.RoomOrder;
            TempData["orderData"] = orderData;

            return RedirectToAction("Index", "CheckOut");
        }

        //public ActionResult DetailAlbum()
        //{
        //    return View();
        //}
    }
}