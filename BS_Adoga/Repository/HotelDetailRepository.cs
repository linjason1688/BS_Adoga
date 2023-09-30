using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelDetail;
using BS_Adoga.Models.ViewModels.HotelImagePage;
using BS_Adoga.Models.ViewModels.Search;
using System.ComponentModel.DataAnnotations;

namespace BS_Adoga.Repository
{
    public class HotelDetailRepository
    {
        private AdogaContext _context;

        public HotelDetailRepository()
        {
            _context = new AdogaContext();
        }

        public IQueryable<Hotel> GetHotelById(string hotelId)
        {
            var hotel = from h in _context.Hotels
                        where h.HotelID == hotelId
                        select h;

            return hotel;

        }

        public string GetHotelIdByName(string hotelName)
        {
            string hotelId = null;
            try
            {
               hotelId = (from h in _context.Hotels
                          where h.HotelName.Contains(hotelName)
                          select h.HotelID).First();
            }
            catch(Exception e)
            {
                var a = e;
            }
            return hotelId;            
        }

        public IQueryable<Facility> GetHotelFacilityById(string hotelId)
        {
            var facility = from h in _context.Hotels
                           join f in _context.Facilities on h.HotelID equals f.HotelID
                           where h.HotelID == hotelId
                           select f;

            return facility;

        }


        public IEnumerable<RoomTypeVM> GetRoomTypeByFilter(string hotelId, DateTime startDate, DateTime endDate, int countNight, int orderRoom, int adult, int child, int totalPerson)
        {
            //1. 先找出符合條件的hotel 和 room
            var table = (from h in _context.Hotels
                         join r in _context.Rooms on h.HotelID equals r.HotelID
                         join r_detail in _context.RoomsDetails on r.RoomID equals r_detail.RoomID
                         where h.HotelID == hotelId && r.NumberOfPeople * orderRoom >= totalPerson && r_detail.CheckInDate >= startDate && r_detail.CheckInDate < endDate
                         select new { HotelID = h.HotelID, r, r_detail }).AsEnumerable();


            //2. 再針對找出來的hotel和room去計算那段日期內的剩餘房間數和折扣。
            //t 裡面除了HotelID 還有  r_detail (RoomDetail) 這2個表格  
            var table_2 = from t in table
                          group new { t.HotelID, t.r_detail } by new { t.HotelID, t.r_detail.RoomID } into roomGroup
                          where roomGroup.Count() >= countNight && roomGroup.Min(r => r.r_detail.RoomCount - r.r_detail.RoomOrder) >= orderRoom
                          select new
                          {
                              HotelID = roomGroup.Key.HotelID,
                              RoomID = roomGroup.Key.RoomID,
                              MinRoom = roomGroup.Min(r => r.r_detail.RoomCount - r.r_detail.RoomOrder),
                              Discount = roomGroup.Sum(r => r.r_detail.RoomDiscount) / countNight
                              //d= roomGroup.Sum(r => r.r_detail.RoomDiscount)
                          };

            //查看裡面每筆的總discount
            //var a = table_2.Select(d => d.d).ToList();
            //a.ForEach(s => { var b = s;
            //    var c = s; });

            //3.查詢出最終的結果並放進view Model
            var finalTable = from t2 in table_2
                             join r in _context.Rooms on t2.HotelID equals r.HotelID
                             join bath in _context.BathroomTypes on r.TypesOfBathroomID equals bath.TypesOfBathroomID
                             where r.RoomID == t2.RoomID
                             orderby (r.RoomPrice * (1 - t2.Discount))
                             select new RoomTypeVM
                             {
                                 HotelID = r.HotelID,
                                 RoomID = r.RoomID,
                                 RoomName = r.RoomName,
                                 RoomImgURL = GetFirstRoomImagesById(t2.HotelID,r.RoomID),
                                 WiFi = r.WiFi,
                                 Breakfast = r.Breakfast,
                                 NoSmoking = r.NoSmoking,
                                 BathroomName = bath.Name,
                                 RoomBed = (from r_bed in _context.RoomBeds
                                            join bed in _context.BedTypes on r_bed.TypesOfBedsID equals bed.TypesOfBedsID
                                            where r_bed.RoomID == r.RoomID
                                            select new RoomBedVM
                                            {
                                                Name = bed.Name,
                                                Amount = r_bed.Amount
                                            }),
                                 Adult = 0,
                                 Child = 0,
                                 RoomOrder = orderRoom,
                                 RoomPrice = r.RoomPrice,
                                 RoomDiscount = 1 - t2.Discount,
                                 RoomNowPrice = (r.RoomPrice * (1 - t2.Discount)),
                                 RoomLeft = t2.MinRoom
                             };
            return finalTable;

            //foreach (var x in table_2)
            //{
            //    string h = x.HotelID;
            //    string room = x.RoomID;
            //    int min = x.MinRoom;
            //    decimal dis = x.Discount;
            //}
        }

        public IEnumerable<ImagesVM> GetHotelImagesById(string hotelId)
        {
            var images = from himg in _context.HotelImageUploads
                         where himg.HotelID == hotelId
                         orderby himg.ImageID
                         select new ImagesVM
                         {
                             ImageID = himg.ImageID,
                             ImageURL = himg.ImageURL
                         };
            return images;
        }

        public IEnumerable<ImagesVM> GetRoomImagesById(string hotelId)
        {
            var images = from rimg in _context.RoomImages
                         where rimg.HotelID == hotelId
                         orderby rimg.ImageID
                         select new ImagesVM
                         {
                             ImageID = rimg.ImageID,
                             ImageURL = rimg.ImageURL
                         };
            return images;
        }

        public string GetFirstHotelImageById(string hotelId)
        {
            var url = (from himg in _context.HotelImageUploads
                       where himg.HotelID == hotelId
                       orderby himg.ImageID
                       select himg.ImageURL).FirstOrDefault();

            if (url == null)
                url = "/Asset/images/no_image.jpg";

            return url;
        }
        public string GetFirstRoomImagesById(string hotelId,string roomId)
        {
            var url = (from rimg in _context.RoomImages
                          where rimg.HotelID == hotelId && rimg.RoomID == roomId
                          orderby rimg.ImageID
                          select rimg.ImageURL).FirstOrDefault();

            if(url == null)
                url = "/Asset/images/no_image.jpg";

            return url;
        }
    }
}