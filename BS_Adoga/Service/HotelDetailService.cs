using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelDetail;
using BS_Adoga.Models.ViewModels.HotelImagePage;
using BS_Adoga.Models.ViewModels.Account;
using BS_Adoga.Repository;

namespace BS_Adoga.Service
{
    public class HotelDetailService
    {
        private readonly AdogaContext _context;
        private readonly HotelDetailRepository _repository;
        private readonly DBRepository _DBrepository;

        public HotelDetailService()
        {
            _context = new AdogaContext();
            _repository = new HotelDetailRepository();
            _DBrepository = new DBRepository(_context);
        }

        public DetailVM GetDetailVM(string hotelId, string startDate, string endDate, int orderRoom, int adult, int child)
        {
            DetailVM hotelDetail = new DetailVM()
            {
                hotelVM = GetHotelById(hotelId),
                roomTypeVM = GetRoomTypeByFilter(hotelId, startDate, endDate, orderRoom, adult, child),
                hotelOptionVM = new SearchCardRepository().GetHotelOption(),
                HotelImages = GetHotelImagesById(hotelId),
                HotelScoreVM = GetScoreById(hotelId)
            };
            return hotelDetail;
        }

        public HotelVM GetHotelById(string hotelId)
        {
            var source = _repository.GetHotelById(hotelId);

            //先first 再轉 view model
            //如果用 first or default要處理default 否則用 first就好
            var result = source.Select(s => new HotelVM
            {
                HotelID = s.HotelID,
                HotelName = s.HotelName,
                HotelEngName = s.HotelEngName,
                HotelCity = s.HotelCity,
                HotelAddress = s.HotelAddress,/*s.HotelCity + "," + s.HotelDistrict + "," +*/
                HotelAbout = s.HotelAbout,
                Longitude = s.Longitude,
                Latitude = s.Latitude,
                Star = s.Star
            }).First();

            return result;
        }

        //根據床型判斷房間可以有多少個大人和小孩
        public List<RoomTypeVM> Helper_CountAdultChild(List<RoomTypeVM> data)
        {
            data.ForEach((x) =>
            {
                foreach (var bed in x.RoomBed)
                {
                    switch (bed.Name)
                    {
                        case "雙人床":
                        case "加大雙人床":
                        case "單人床(兩床)":
                            //result.Where((x,index)=>index==count).
                            x.Adult = x.Adult + (2 * bed.Amount);
                            x.Child = x.Child + (1 * bed.Amount);
                            break;

                        case "特大雙人床":
                            x.Adult = x.Adult + (2 * bed.Amount);
                            x.Child = x.Child + (2 * bed.Amount);
                            break;

                        case "上下舖":
                            x.Adult = x.Adult + (2 * bed.Amount);
                            x.Child = x.Child + 0;
                            break;
                        case "單人床":
                            x.Adult = x.Adult + (1 * bed.Amount);
                            x.Child = x.Child + 0;
                            break;

                        default:
                            break;
                    }
                }
            });

            return data;
        }
        //搜尋出符合user輸入的條件的房型。
        public IEnumerable<RoomTypeVM> GetRoomTypeByFilter(string hotelId, string startDate, string endDate, int orderRoom, int adult, int child)
        {
            //設定好傳給repository的引數。
            if (hotelId == null) hotelId = "hotel04";
            DateTime startDate_p = DateTime.Parse(startDate);
            DateTime endDate_p = DateTime.Parse(endDate);
            int countNight = new TimeSpan(endDate_p.Ticks - startDate_p.Ticks).Days;//2;
            //int orderRoom = 2;
            int totalPerson = adult + child;//12

            var result = _repository.GetRoomTypeByFilter(hotelId, startDate_p, endDate_p, countNight, orderRoom, adult, child, totalPerson).ToList();

            result = Helper_CountAdultChild(result);

            return result;
        }
        //篩選特定房型（禁煙，免費早餐，家庭房）
        public IEnumerable<RoomTypeVM> GetSpecificRoomType(string hotelId, string startDate, string endDate, int orderRoom, int adult, int child, bool breakfast, bool noSmoking, bool family)
        {
            //這隻service要做的事情跟別的service有重複到，就直接讓那個service處理先
            var allRoom = GetRoomTypeByFilter(hotelId, startDate, endDate, orderRoom, adult, child).ToList();

            var result = from room in allRoom
                         select room;
            if (breakfast)
            {
                result = from room in result
                         where room.Breakfast == breakfast
                         select room;
            }
            if (noSmoking)
            {
                result = from room in result
                         where room.NoSmoking == noSmoking
                         select room;
            }
            if (family)
            {
                int ppl = 4;
                result = from room in result
                         where room.Adult + room.Child >= ppl
                         select room;
            }

            return result;
        }

        public object GetHotelFacilityById(string hotelId)
        {
            var source = _repository.GetHotelFacilityById(hotelId);

            var result = source.Select(x => new
            {
                x.AirportTransfer,
                x.BusinessFacilities,
                x.CarPark,
                x.FacilitiesFordisabledGuests,
                x.FamilyChildFriendly,
                x.FrontDesk,
                x.GolfCourse,
                x.Gym,
                x.Internet,
                x.Nightclub,
                x.NoSmoking,
                x.PetsAllowed,
                x.Restaurants,
                x.SmokingArea,
                x.SpaSauna,
                x.SwimmingPool
            }).First();

            return result;
        }

        public string[] GetHotelImagesById(string hotelId)
        {
            var HotelImages = _repository.GetHotelImagesById(hotelId);
            int imgMaxLength = 4;
            string[] hotelImgURL = new string[imgMaxLength];

            for (int i = 0; i < imgMaxLength; i++)
            {
                if (i < HotelImages.Count())
                {
                    hotelImgURL[i] = HotelImages.Where((x, index) => index == i).First().ImageURL;
                }
                else
                {
                    hotelImgURL[i] = "/Asset/images/no_image.jpg";
                }
            }

            return hotelImgURL;
        }

        public IEnumerable<ImagesVM> GetRoomImagesById(string hotelId,string roomId)
        {
            var roomImages = (from rimg in _context.RoomImages
                                          where rimg.HotelID == hotelId && rimg.RoomID == roomId
                                          orderby rimg.ImageID
                                          select new ImagesVM
                                          {
                                              ImageID = rimg.ImageID,
                                              ImageURL = rimg.ImageURL
                                          }).AsEnumerable();
            
            if(roomImages.Count() == 0)
            {
                var data = new ImagesVM[]{ new ImagesVM() { ImageID = "none", ImageURL = "/Asset/images/no_image.jpg" }};
                return data;
            }
            return roomImages;
        }

        public ScoreVM GetScoreById(string hotelId)
        {
            var source = _DBrepository.GetAll<MessageBoard>().Where(x => x.HotelID == hotelId);

            int allCount = source.Count();
            int goodCount = source.Where(x => x.Score >= 7).Count();
            decimal avg = 0.0m;
            int percent = 0;
            string level = "暫無評分";
            if (source.Count() != 0)
            {
                avg = decimal.Round(source.Average(x => (decimal)x.Score), 1, MidpointRounding.AwayFromZero);
                double a = ((double)goodCount / (double)allCount);
                percent = (int)Math.Round(a * 100, 0, MidpointRounding.AwayFromZero);
                if (avg <= 3)
                    level = "很差";
                else if(avg >3 && avg <= 5)
                    level = "不好";
                else if (avg > 5 && avg <= 7)
                    level = "還行";
                else if (avg > 7 && avg <= 8)
                    level = "還不錯";
                else if (avg > 8 && avg <= 9.5m)
                    level = "很讚";
                else if (avg > 9.5m)
                    level = "超讚";
            }

            var data = new ScoreVM()
            {
                AllMessageCount = allCount,
                HighScoreMessageCount = goodCount,
                HighScorePercent = percent,
                ScoreAvg = avg,
                ScoreLevel = level
            };

            return data;
        }

        public IEnumerable<EvaluationPageViewModel> GetHotelMessageById(string hotelId)
        {
            var table = (from m in _context.MessageBoards.AsEnumerable()
                         join h in _context.Hotels on m.HotelID equals h.HotelID
                         join o in _context.Orders on m.OrderID equals o.OrderID
                         join r in _context.Rooms on o.RoomID equals r.RoomID
                         where m.HotelID == hotelId
                         select new EvaluationPageViewModel
                         {
                             OrderID = m.OrderID,
                             HotelID = m.HotelID,
                             CustomerID = m.CustomerID,
                             Title = m.Title,
                             MessageText = m.MessageText.ToString(),
                             MessageDate = m.MessageDate.ToString("yyyy年MM月dd日") + m.MessageDate.ToString(" dddd"),
                             Score = ((decimal)m.Score).ToString("#0.0"),
                             CustomerName = ($"{o.FirstName} {o.LastName}"),
                             HotelName = h.HotelName,
                             RoomName = r.RoomName,
                             Stay = o.CheckOutDate.Subtract(o.CheckInDate).ToString("%d")
                         });

            return table;
        }

    }
}