using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelLogin;
using BS_Adoga.Models.ViewModels.MemberLogin;
using BS_Adoga.Models.ViewModels.HotelImagePage;
using BS_Adoga.Repository;
using BS_Adoga.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


//Hotel-Back-End
namespace BS_Adoga.Controllers
{
    [HotelLoginAuthorize]
    public class FunctionController : Controller
    {
        private AdogaContext _context;
        private FunctionRepository _repository;
        private FunctionService _service;

        public FunctionController()
        {
            _context = new AdogaContext();
            _repository = new FunctionRepository(_context);
            _service = new FunctionService();
        }

        // GET: Function
        //public ActionResult Index2()
        //{
        //    string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
        //    UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
        //    string id = UserCookie.Id;
        //    string picture = UserCookie.PictureUrl;
        //    ViewBag.id = id;
        //    ViewBag.pictureurl = picture;
        //    return View();
        //}
        public ActionResult Index()
        {
            return View();
        }

        //顯示所有飯店
        public ActionResult HotelIndex()
        {
            //取得HotelEmpID
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            //取得所有飯店設施清單
            List<Facility> facilities = _context.Facilities.ToList();
            ViewBag.Facilities = facilities;


            return View(_repository.GetHotelListByEmpID(user_id));

            #region 顯示所有飯店清單_舊版本
            //取得所有飯店設施清單
            //List<Facility> facilities = _context.Facilities.ToList();
            //ViewBag.Facilities = facilities;
            //return View(_repository.GetHotelList());  //取得所有Hotel資料
            #endregion
        }

        //飯店建立
        public ActionResult HotelCreate()
        {
            //縣市連動區域的選項
            List<SimpleZipCodeVM> Citys = JsonConvert.DeserializeObject<List<SimpleZipCodeVM>>(_service.CityJSON());
            List<SelectListItem> firstitems = new List<SelectListItem>();
            List<SelectListItem> seconditems = new List<SelectListItem>();
            foreach (var item in Citys)
            {
                firstitems.Add(new SelectListItem()
                {
                    Text = item.city,
                    Value = item.city
                });
                if (item.city == "臺北市")
                {
                    foreach (var item2 in item.districts)
                        seconditems.Add(new SelectListItem()
                        {
                            Text = item2.district,
                            Value = item2.district
                        });
                }
            }
            ViewBag.firstitems = firstitems;
            ViewBag.seconditems = seconditems;

            return View();
        }

        //飯店建立
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HotelCreate(HotelCreateViewModel HotelCreateVM)
        {
            string username = User.Identity.Name;
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            if (ModelState.IsValid)
            {
                Hotel hotel = new Hotel()
                {
                    HotelID = HotelCreateVM.HotelID,
                    HotelName = HotelCreateVM.HotelName,
                    HotelEngName = HotelCreateVM.HotelEngName,
                    HotelCity = HotelCreateVM.HotelCity,
                    HotelDistrict = HotelCreateVM.HotelDistrict,
                    HotelAddress = HotelCreateVM.HotelAddress,
                    HotelAbout = HotelCreateVM.HotelAbout,
                    Longitude = HotelCreateVM.Longitude,
                    Latitude = HotelCreateVM.Latitude,
                    Star = HotelCreateVM.Star,
                    Logging = "建立" + "," + username + "," + DateTime.Now.ToString()
                };
                HotelEmpMappingHotel Mapping = new HotelEmpMappingHotel()
                {
                    MappingID = HotelCreateVM.HotelID + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"),
                    HotelID = HotelCreateVM.HotelID,
                    HotelEmployeeID = user_id
                };

                _context.Hotels.Add(hotel);
                _context.HotelEmpMappingHotels.Add(Mapping);
                _context.SaveChanges();
                return RedirectToAction("HotelIndex");
            }
            return View(HotelCreateVM);
        }

        //顯示所有飯店
        public ActionResult HotelList()
        {
            return View(_context.Hotels.ToList());
        }

        //飯店詳細說明
        //Route: Hotel/Detail/{hotelid}
        public ActionResult HotelDetails(string hotelid)
        {
            if (hotelid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = _context.Hotels.Find(hotelid);

            if (hotel == null)
            {
                return HttpNotFound();
            }

            return View(hotel);
        }

        //飯店修改
        public ActionResult HotelEdit(string hotelid)
        {
            if (hotelid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = _context.Hotels.Find(hotelid);
            HotelCreateViewModel hotelCreateVM = new HotelCreateViewModel()
            {
                HotelID = hotel.HotelID,
                HotelName = hotel.HotelName,
                HotelEngName = hotel.HotelEngName,
                HotelCity = hotel.HotelCity,
                HotelDistrict = hotel.HotelDistrict,
                HotelAddress = hotel.HotelAddress,
                HotelAbout = hotel.HotelAbout,
                Longitude = hotel.Longitude,
                Latitude = hotel.Latitude,
                Star = hotel.Star,
                Logging = hotel.Logging
            };
            if (hotel == null)
            {
                return HttpNotFound();
            }

            List<SimpleZipCodeVM> Citys = JsonConvert.DeserializeObject<List<SimpleZipCodeVM>>(_service.CityJSON());
            List<SelectListItem> firstitems = new List<SelectListItem>();
            List<SelectListItem> seconditems = new List<SelectListItem>();
            foreach (var item in Citys)
            {
                firstitems.Add(new SelectListItem()
                {
                    Text = item.city,
                    Value = item.city,
                    Selected = item.city.Equals(hotel.HotelCity)
                });
                if (item.city == hotel.HotelCity)
                {
                    foreach (var item2 in item.districts)
                        seconditems.Add(new SelectListItem()
                        {
                            Text = item2.district,
                            Value = item2.district,
                            Selected = item2.district.Equals(hotel.HotelDistrict)
                        });
                }
            }
            ViewBag.firstitems = firstitems;
            ViewBag.seconditems = seconditems;

            return View(hotelCreateVM);
        }

        //飯店修改
        //Route: Hotel/Edit/{hotelid}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HotelEdit(HotelCreateViewModel hotelCreateVM)
        {
            #region 重構後的作法
            if (ModelState.IsValid)
            {
                var service = new FunctionService();
                var result = service.HotelEdit(hotelCreateVM, User.Identity.Name);
                if (result.IsSuccessful)
                {
                    return RedirectToAction("HotelIndex");
                }
                else
                {
                    var Log = result.WriteLog();
                    return Content("編輯失敗:" + Log);
                }
                //return View(hotelCreateVM);
            }
            else
            {
                return View(hotelCreateVM);
            }
            #endregion
        }

        //動態取得區域資料
        //第二個區動態取得資料
        public ActionResult SecondItems(string city)
        {
            List<SimpleZipCodeVM> Citys = JsonConvert.DeserializeObject<List<SimpleZipCodeVM>>(_service.CityJSON());
            List<SelectListItem> firstitems = new List<SelectListItem>();
            List<SelectListItem> seconditems = new List<SelectListItem>();
            foreach (var item in Citys)
            {
                firstitems.Add(new SelectListItem()
                {
                    Text = item.city,
                    Value = item.city
                });
                if (item.city == city)
                {
                    foreach (var item2 in item.districts)
                        seconditems.Add(new SelectListItem()
                        {
                            Text = item2.district,
                            Value = item2.district
                        });
                }
            }

            var sop = seconditems;
            TagBuilder tb = new TagBuilder("select");
            tb.GenerateId("Select2");
            tb.MergeAttribute("name", "HotelDistrict");
            foreach (var item in sop)
            {
                tb.InnerHtml += "<option value='" + item.Value.ToString() + "'>" + item.Text.ToString() + "</option>";

            }
            return Content(tb.ToString());

        }

        //顯示所有的飯店設施
        public ActionResult HotelFacilityIndex()
        {
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            return View(_repository.GetHotelFaciliyByEmpID(user_id));

            #region 顯示所有飯店設施清單_舊版本
            //顯示所有的飯店設施清單
            //var facilities = _context.Facilities.Include(f => f.Hotel);
            //return View(facilities.ToList());
            #endregion
        }

        //飯店設施建立
        public ActionResult HotelFacilityCreate(string hotelids)
        {
            if (string.IsNullOrEmpty(hotelids))
            {
                ViewBag.HotelID = new SelectList(_context.Hotels, "HotelID", "HotelName");
            }
            else
            {
                ViewBag.HotelID = new SelectList(_context.Hotels.Where(x => x.HotelID == hotelids), "HotelID", "HotelName");
            }

            return View();
        }

        //飯店設施建立
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HotelFacilityCreate(Facility facility)
        {

            facility.Logging = "建立" + "," + User.Identity.Name + "," + DateTime.Now.ToString();
            if (facility.Logging != null)
            {
                _context.Facilities.Add(facility);
                _context.SaveChanges();
                return RedirectToAction("HotelIndex");
            }
            ViewBag.HotelID = new SelectList(_context.Hotels.Where(x => x.HotelID == facility.HotelID), "HotelID", "HotelName");
            return View(facility);
        }

        //飯店設施詳細說明
        public ActionResult HotelFacilityDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = _context.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        //飯店設施修改
        public ActionResult HotelFacilityEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = _context.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelID = new SelectList(_context.Hotels.Where(x => x.HotelID == facility.HotelID), "HotelID", "HotelName", facility.HotelID);
            return View(facility);
        }

        //飯店設施修改
        // POST: Facilities/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HotelFacilityEdit([Bind(Include = "FacilitieID,HotelID,SwimmingPool,AirportTransfer,FamilyChildFriendly,Restaurants,Nightclub,GolfCourse,Internet,Gym,NoSmoking,SmokingArea,FacilitiesFordisabledGuests,CarPark,FrontDesk,SpaSauna,PetsAllowed,BusinessFacilities,Logging")] Facility facility)
        {
            facility.Logging = facility.Logging + ";" + "修改" + "," + User.Identity.Name + "," + DateTime.Now.ToString();
            if (ModelState.IsValid)
            {
                _context.Entry(facility).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("HotelFacilityEdit", facility);
            }
            ViewBag.HotelID = new SelectList(_context.Hotels.Where(x => x.HotelID == facility.HotelID), "HotelID", "HotelName", facility.HotelID);
            return View(facility);
        }

        //顯示所有的飯店
        public ActionResult HotelRoomIndex()
        {
            //List<Facility> facilities = _context.Facilities.ToList();
            //ViewBag.Facilities = facilities;

            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            //ViewBag.URL = $"/Hotel/Room/{_repository.GetHotelRoomCountByEmpID(user_id).FirstOrDefault().HotelID}";
            return View(_repository.GetHotelRoomCountByEmpID(user_id));

            //顯示所有Hotel跟房型數量
            //return View(_repository.GetHotelRoomCount());
        }

        //顯示某飯店所有的房型
        // Route: Hotel/Room/{hotelid}
        public ActionResult HotelRoomsIndex(string hotelid)
        {
            //List<Facility> facilities = _context.Facilities.ToList();
            //ViewBag.Facilities = facilities;
            //var test = _repository.GetHotelRoomAll(hotelid);
            var hotel = _repository.GetHotelList().Where(x => x.HotelID == hotelid).FirstOrDefault();
            ViewBag.hotelname = hotel.HotelName;

            return View(_repository.GetHotelRoomAll(hotelid));
        }

        //房型建立
        public ActionResult HotelRoomCreate(string hotelids)
        {
            if (string.IsNullOrEmpty(hotelids))
            {
                ViewBag.HotelID = new SelectList(_context.Hotels, "HotelID", "HotelName");
            }
            else
            {
                ViewBag.HotelID = new SelectList(_context.Hotels.Where(x => x.HotelID == hotelids), "HotelID", "HotelName");
            }
            ViewBag.TypesOfBathroomID = new SelectList(_context.BathroomTypes, "TypesOfBathroomID", "Name");
            ViewBag.HotelIDs = hotelids;
            return View();
        }

        //房型建立
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HotelRoomCreate(HotelRoomCreateViewModel hotelRoomCreateVM)
        {
            if (ModelState.IsValid)
            {
                Room room = new Room()
                {
                    RoomID = hotelRoomCreateVM.HotelID + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    HotelID = hotelRoomCreateVM.HotelID,
                    RoomName = hotelRoomCreateVM.RoomName,
                    NumberOfPeople = hotelRoomCreateVM.NumberOfPeople,
                    RoomCount = hotelRoomCreateVM.RoomCount,
                    RoomPrice = hotelRoomCreateVM.RoomPrice,
                    TypesOfBathroomID = hotelRoomCreateVM.TypesOfBathroomID,
                    NoSmoking = hotelRoomCreateVM.NoSmoking,
                    Breakfast = hotelRoomCreateVM.Breakfast,
                    WiFi = hotelRoomCreateVM.WiFi,
                    TV = hotelRoomCreateVM.TV,
                    Logging = "建立" + "," + User.Identity.Name + "," + DateTime.Now.ToString()
                };
                _context.Rooms.Add(room);
                _context.SaveChanges();
                return RedirectToAction("HotelRoomIndex");
            }

            ViewBag.HotelID = new SelectList(_context.Hotels.Where(x => x.HotelID == hotelRoomCreateVM.HotelID), "HotelID", "HotelName");
            ViewBag.TypesOfBathroomID = new SelectList(_context.BathroomTypes, "TypesOfBathroomID", "Name");
            return View(hotelRoomCreateVM);
        }

        //房型詳細說明
        public ActionResult HotelRoomDetails(string roomid)
        {
            if (roomid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = _context.Rooms.Find(roomid);

            //顯示所有床型名稱跟數量
            string BedNameString = string.Empty;
            foreach (var item in _repository.GetRoomBeds(roomid))
            {
                BedNameString = string.Join("", BedNameString + $"{item.Name}X{item.Amount}，");
            }
            if (!string.IsNullOrEmpty(BedNameString)) BedNameString = BedNameString.Remove(BedNameString.LastIndexOf("，"), 1);
            else BedNameString = "空";
            ViewBag.BedNameString = BedNameString;

            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        //房型資料修改
        //Route: Hotel/Room/Edit/{hotelids}-{roomid}
        public ActionResult HotelRoomEdit(string hotelids, string roomid)
        {
            TempData["roomid"] = roomid;
            Room room = _context.Rooms.Find(roomid);
            HotelRoomCreateViewModel hotelRoomCreateVM = new HotelRoomCreateViewModel()
            {
                HotelID = room.HotelID,
                RoomName = room.RoomName,
                NumberOfPeople = room.NumberOfPeople,
                RoomCount = room.RoomCount,
                RoomPrice = room.RoomPrice,
                TypesOfBathroomID = room.TypesOfBathroomID,
                NoSmoking = room.NoSmoking,
                Breakfast = room.Breakfast,
                WiFi = room.WiFi,
                TV = room.TV
            };

            TempData["Logging"] = room.Logging;

            if (string.IsNullOrEmpty(hotelids))
            {
                ViewBag.HotelID = new SelectList(_context.Hotels, "HotelID", "HotelName");
            }
            else
            {
                ViewBag.HotelID = new SelectList(_context.Hotels.Where(x => x.HotelID == hotelids), "HotelID", "HotelName");
            }
            ViewBag.TypesOfBathroomID = new SelectList(_context.BathroomTypes.Where(x => x.TypesOfBathroomID == room.TypesOfBathroomID), "TypesOfBathroomID", "Name");
            return View(hotelRoomCreateVM);
        }

        //房型資料修改
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HotelRoomEdit(HotelRoomCreateViewModel hotelRoomCreateVM)
        {
            var roomid = TempData["roomid"].ToString();
            string Logging = TempData["Logging"].ToString();

            if (ModelState.IsValid)
            {
                Room room = new Room()
                {
                    RoomID = roomid,
                    HotelID = hotelRoomCreateVM.HotelID,
                    RoomName = hotelRoomCreateVM.RoomName,
                    NumberOfPeople = hotelRoomCreateVM.NumberOfPeople,
                    RoomCount = hotelRoomCreateVM.RoomCount,
                    RoomPrice = hotelRoomCreateVM.RoomPrice,
                    TypesOfBathroomID = hotelRoomCreateVM.TypesOfBathroomID,
                    NoSmoking = hotelRoomCreateVM.NoSmoking,
                    Breakfast = hotelRoomCreateVM.Breakfast,
                    WiFi = hotelRoomCreateVM.WiFi,
                    TV = hotelRoomCreateVM.TV,
                    Logging = Logging + ";" + "修改" + "," + User.Identity.Name + "," + DateTime.Now.ToString()
                };

                _context.Entry(room).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("HotelRoomEdit", hotelRoomCreateVM);
            }

            ViewBag.HotelID = new SelectList(_context.Hotels.Where(x => x.HotelID == hotelRoomCreateVM.HotelID), "HotelID", "HotelName");
            ViewBag.TypesOfBathroomID = new SelectList(_context.BathroomTypes.Where(x => x.TypesOfBathroomID == hotelRoomCreateVM.TypesOfBathroomID), "TypesOfBathroomID", "Name");
            return View(hotelRoomCreateVM);
        }

        //每日房間清單的日曆頁面
        public ActionResult RoomDetailsIndex(string roomid)
        {
            if (_context.Rooms.Where(x => x.RoomID == roomid).FirstOrDefault() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            else
            {
                ViewBag.roomid = roomid;
                ViewBag.roomname = _context.Rooms.Where(x => x.RoomID == roomid).FirstOrDefault().RoomName;

                List<RoomsDetail> RoomDetailThisMonth = _repository.GetAllRoomDetailThisMonth(roomid).ToList();
                ViewBag.RoomDetailThisMonthJSON = JsonConvert.SerializeObject(RoomDetailThisMonth);

                return View(RoomDetailThisMonth);
            }
        }

        //房間月的展開
        //Route: Hotel/Room/Detail/Expansion/{year}-{month}-{roomid}
        public ActionResult RoomDetailExpansion(string year, string month, string roomid)
        {
            string username = User.Identity.Name; //取得使用者名稱Logging要記錄誰修改

            //如果是不存在的roomid返回錯誤頁面
            if (_context.Rooms.Where(x => x.RoomID == roomid).FirstOrDefault() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //如果存在則繼續
            else
            {
                string RDID = year + "-" + month + "-1-" + roomid;

                var test = _context.RoomsDetails.Where(x => x.RDID == "2021-06-20room01").FirstOrDefault();
                var DBRoomDetailData = _context.RoomsDetails.Where(x => x.RDID == RDID).FirstOrDefault();

                //判斷當月第一天是否已經有展開過(有展開過代表整個月已有資料)
                if (DBRoomDetailData == null)
                {
                    string CurrentDate = $"{year}/{month}/30 14:00:00";
                    DateTime DateObject = Convert.ToDateTime(CurrentDate);

                    OperationResult CreatResult = _service.CreateRoomDetailExpansion(year, month, roomid, username);

                    return Redirect($"~/Function/RoomDetailsIndex?roomid={roomid}");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

            }
        }

        //房間床型新增
        public ActionResult RoomBedCreate(string roomid)
        {
            var hotelid = _context.Rooms.Where(x => x.RoomID == roomid).FirstOrDefault();
            ViewBag.hotelid = hotelid.HotelID;
            ViewBag.TypesOfBedsID = new SelectList(_context.BedTypes, "TypesOfBedsID", "Name");
            ViewBag.RoomID = new SelectList(_context.Rooms.Where(x => x.RoomID == roomid), "RoomID", "RoomName");
            return View();
        }

        //房間床型新增
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoomBedCreate([Bind(Include = "RoomID,TypesOfBedsID,Amount")] RoomBed roomBed)
        {
            var hotelid = _context.Rooms.Where(x => x.RoomID == roomBed.RoomID).FirstOrDefault();
            ViewBag.hotelid = hotelid.HotelID;
            if (ModelState.IsValid)
            {
                _context.RoomBeds.Add(roomBed);
                _context.SaveChanges();
                return Redirect($"~/Hotel/Room/{hotelid.HotelID}");
            }

            ViewBag.TypesOfBedsID = new SelectList(_context.BedTypes, "TypesOfBedsID", "Name", roomBed.TypesOfBedsID);
            ViewBag.RoomID = new SelectList(_context.Rooms.Where(x => x.RoomID == roomBed.RoomID), "RoomID", "RoomName", roomBed.RoomID);
            return View(roomBed);
        }


        //房間床型刪除
        public ActionResult RoomBedDelete(string roomid)
        {
            var roomBeds = _context.RoomBeds.Where(x => x.RoomID == roomid).ToList();

            if (roomBeds != null)
            {
                foreach (var item in roomBeds)
                {
                    _context.RoomBeds.Remove(item);
                    _context.SaveChanges();
                }
            }

            var hotelid = _context.Rooms.Where(x => x.RoomID == roomid).FirstOrDefault();
            return Redirect($"~/Hotel/Room/{hotelid.HotelID}");
        }

        [HttpGet]
        public ActionResult HotelImagePage()
        {
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            var result = _service.GetHotelImageDataByUserId(user_id);
            return View(result);
        }

        //顯示所有訂單 BY UserID
        public ActionResult OrderAllData()
        {
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            List<OrderViewModel> OrderData = _repository.GetAllOrderByEmpID(user_id).ToList();
            ViewBag.OrderJSON = JsonConvert.SerializeObject(OrderData);

            ViewBag.userID = user_id;
            return View("OrderIndex", null);
        }

        //顯示所有Hotel未入住的訂單
        public ActionResult OrderNotCheckIn()
        {
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            List<OrderViewModel> OrderData = _repository.GetOrderNotCheckIn(user_id).ToList();
            ViewBag.OrderJSON = JsonConvert.SerializeObject(OrderData);

            ViewBag.userID = user_id;
            return View("OrderIndex", null);
        }

        //顯示所有Hotel已入住的訂單
        public ActionResult OrderCheckIn()
        {
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            List<OrderViewModel> OrderData = _repository.GetOrderCheckIn(user_id).ToList();
            ViewBag.OrderJSON = JsonConvert.SerializeObject(OrderData);

            ViewBag.userID = user_id;
            return View("OrderIndex", null);
        }

        //顯示所有Hotel未付款的訂單
        public ActionResult OrderNotPay()
        {
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            List<OrderViewModel> OrderData = _repository.GetOrderNotPay(user_id).ToList();
            ViewBag.OrderJSON = JsonConvert.SerializeObject(OrderData);

            ViewBag.userID = user_id;
            return View("OrderIndex", null);
        }



        [HttpGet]
        public ActionResult RoomImagePage()
        {
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            var result = new RoomImageVM()
            {
                HotelEmployeeID = user_id,
                HotelOptions = _service.GetHotelOptionByUserID(user_id)
            };

            return View(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}