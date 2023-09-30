using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.homeViewModels;
using Microsoft.AspNetCore.Http.Features;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Repository
{
    public class HomeRepository
    {
        public AdogaContext _context;
        public HomeRepository()
        {
            _context = new AdogaContext();
        }
        List<Card> cards = new List<Card>
        {
            new Card { name = "台中市",ImageURL="../Asset/images/Home/台中市.png" },
            new Card { name = "台南市",ImageURL="../Asset/images/Home/台南市.png" },
            new Card { name = "花蓮縣",ImageURL="../Asset/images/Home/花蓮縣.png" },
            new Card { name = "高雄市",ImageURL="../Asset/images/Home/高雄市.png" },
            new Card { name = "新北市",ImageURL="../Asset/images/Home/新北市.png" },
            new Card { name = "新竹縣",ImageURL="../Asset/images/Home/新竹縣.png" },
            new Card { name = "台東縣",ImageURL="../Asset/images/Home/台東縣.png" },
           
        };
        public IQueryable<CardViewModels> GetCardModels3()
        {
            var images = (from p in _context.Hotels
                         join d in _context.Rooms on p.HotelID equals d.HotelID
                          join s in _context.RoomImages on d.RoomID equals s.RoomID
                          join z in _context.RoomsDetails on d.RoomID equals z.RoomID
                          join v in _context.HotelImageUploads on s.ImageID equals v.ImageID
                          select new CardViewModels
                         {
                             HotelID = p.HotelID,
                             HotelName = p.HotelName,
                             HotelCity = p.HotelCity,
                             Star = p.Star,
                              ImageURL = (from x in _context.HotelImageUploads

                                          where x.HotelID == p.HotelID
                                          orderby x.ImageID
                                          select x.ImageURL

                                               ).FirstOrDefault(),
                              My_Rooms = new MyRoom
                             {
                                 HotelID = p.HotelID,
                                 RoomPrice = d.RoomPrice
                             },
                             My_RoomsDetails = new MyRoomsDetails
                             {
                                 RoomID = d.RoomID,
                                 RoomDiscount = z.RoomDiscount
                             }
                         }).Take(8);
            return images;
        }

        public IEnumerable<CardViewModels> GetCardModels2()
        {
            var images = (from p in _context.Hotels
                          orderby p.HotelID
                          select new CardViewModels
                          {                           
                              HotelID = p.HotelID,
                              HotelName = p.HotelName,
                              HotelCity = p.HotelCity,
                              Star = p.Star
                          }).Take(4).ToList();
            foreach(var item in images)
            {
                item.ImageURL = (from x in _context.HotelImageUploads

                                 where x.HotelID == item.HotelID
                                 orderby x.ImageID
                                 select x.ImageURL).FirstOrDefault();
                item.My_Rooms = (from d in _context.Rooms
                                 where d.HotelID == item.HotelID
                                 select new MyRoom
                                 {
                                     HotelID = d.HotelID,
                                     RoomPrice = d.RoomPrice
                                 }).FirstOrDefault();
                string rid = (from d in _context.Rooms
                              where d.HotelID == item.HotelID
                              select d.RoomID).FirstOrDefault();
                item.My_RoomsDetails = (from z in _context.RoomsDetails 
                                        where z.RoomID == rid
                                        select new MyRoomsDetails
                                        {
                                            RoomID = z.RoomID,
                                            RoomDiscount = z.RoomDiscount
                                        }).FirstOrDefault();
                item.ddd = 5.0m;
               var ddd =(from m in _context.MessageBoards
                           where m.HotelID ==item.HotelID
                                      select m.Score
                                     );
                if (ddd.Count() != 0)
                {
                    item.ddd = decimal.Round((decimal)ddd.Average(),1,MidpointRounding.AwayFromZero);
                    int x = 0;
                }
            }

            return images.ToList();
        }
        public IQueryable<CardViewModels> GetCardModels4(string cardlocal)
        {
            var result = from p in _context.Hotels
                         join d in _context.Rooms on p.HotelID equals d.HotelID
                         where p.HotelCity == cardlocal
                         orderby p.HotelCity

                         select new CardViewModels
                         {
                             HotelID = p.HotelID,
                             HotelName = p.HotelName,
                             HotelCity = p.HotelCity,
                             HotelAddress = p.HotelAddress,
                             HotelEngName = p.HotelEngName,
                               My_Rooms = new MyRoom
                               {
                                   HotelID = p.HotelID,
                                   NoSmoking = d.NoSmoking,
                                   Breakfast = d.Breakfast,
                                   WiFi =d.WiFi,
                                   RoomName=d.RoomName
                               },
                               ImageURL=(from x in _context.HotelImageUploads
                                         where x.HotelID == p.HotelID
                                         orderby x.ImageID
                                         select x.ImageURL).FirstOrDefault()

        };
            return result;
        }
            public IEnumerable<CardViewModels> GetCardModels(string cardlocal)
        {
            var city = from p in _context.Hotels
                       where p.HotelCity == cardlocal
                       select p.HotelCity;
            if (cardlocal == null)
            {
                var images = (from p in _context.Hotels
                              orderby p.HotelID
                              select new CardViewModels
                              {
                                  HotelID = p.HotelID,
                                  HotelName = p.HotelName,
                                  HotelCity = p.HotelCity,
                                  Star = p.Star
                              }).Take(4).ToList();
                foreach (var item in images)
                {
                    item.ImageURL = (from x in _context.HotelImageUploads

                                     where x.HotelID == item.HotelID
                                     orderby x.ImageID
                                     select x.ImageURL).FirstOrDefault();
                    item.My_Rooms = (from d in _context.Rooms
                                     where d.HotelID == item.HotelID
                                     select new MyRoom
                                     {
                                         HotelID = d.HotelID,
                                         RoomPrice = d.RoomPrice
                                     }).FirstOrDefault();
                    string rid = (from d in _context.Rooms
                                  where d.HotelID == item.HotelID
                                  select d.RoomID).FirstOrDefault();
                    item.My_RoomsDetails = (from z in _context.RoomsDetails
                                            where z.RoomID == rid
                                            select new MyRoomsDetails
                                            {
                                                RoomID = z.RoomID,
                                                RoomDiscount = z.RoomDiscount
                                           }).FirstOrDefault();
                    item.ddd = 5.0m;
                    var ddd = (from m in _context.MessageBoards
                               where m.HotelID == item.HotelID
                               select m.Score
                                          );
                    if (ddd.Count() != 0)
                    {
                        item.ddd = decimal.Round((decimal)ddd.Average(), 1, MidpointRounding.AwayFromZero);
                        int i = 0;
                    }
                }

                return images.ToList();
            }
            else if (cardlocal == "")
            {
                var images = (from p in _context.Hotels
                              orderby p.HotelID
                              select new CardViewModels
                              {
                                  HotelID = p.HotelID,
                                  HotelName = p.HotelName,
                                  HotelCity = p.HotelCity,
                                  Star = p.Star
                              }).Take(4).ToList();
                foreach (var item in images)
                {
                    item.ImageURL = (from x in _context.HotelImageUploads

                                     where x.HotelID == item.HotelID
                                     orderby x.ImageID
                                     select x.ImageURL).FirstOrDefault();
                    item.My_Rooms = (from d in _context.Rooms
                                     where d.HotelID == item.HotelID
                                     select new MyRoom
                                     {
                                         HotelID = d.HotelID,
                                         RoomPrice = d.RoomPrice
                                     }).FirstOrDefault();
                    string rid = (from d in _context.Rooms
                                  where d.HotelID == item.HotelID
                                  select d.RoomID).FirstOrDefault();
                    item.My_RoomsDetails = (from z in _context.RoomsDetails
                                            where z.RoomID == rid
                                            select new MyRoomsDetails
                                            {
                                                RoomID = z.RoomID,
                                                RoomDiscount = z.RoomDiscount
                                            }).FirstOrDefault();
                    item.ddd = 5.0m;
                    var ddd = (from m in _context.MessageBoards
                               where m.HotelID == item.HotelID
                               select m.Score
                                          );
                    if (ddd.Count() != 0)
                    {
                        item.ddd = decimal.Round((decimal)ddd.Average(), 1, MidpointRounding.AwayFromZero);
                        int i = 0;
                    }
                }

                return images.ToList();
            }         
            else if (city.Count()>0)
            {
                var images = (from p in _context.Hotels
                              where p.HotelCity ==cardlocal
                              orderby p.HotelID
                              select new CardViewModels
                              {
                                  HotelID = p.HotelID,
                                  HotelName = p.HotelName,
                                  HotelCity = p.HotelCity,
                                  Star = p.Star
                              }).Take(4).ToList();
                foreach (var item in images)
                {
                    item.ImageURL = (from x in _context.HotelImageUploads

                                     where x.HotelID == item.HotelID
                                     orderby x.ImageID
                                     select x.ImageURL).FirstOrDefault();
                    item.My_Rooms = (from d in _context.Rooms
                                     where d.HotelID == item.HotelID
                                     select new MyRoom
                                     {
                                         HotelID = d.HotelID,
                                         RoomPrice = d.RoomPrice
                                     }).FirstOrDefault();
                    string rid = (from d in _context.Rooms
                                  where d.HotelID == item.HotelID
                                  select d.RoomID).FirstOrDefault();
                    item.My_RoomsDetails = (from z in _context.RoomsDetails
                                            where z.RoomID == rid
                                            select new MyRoomsDetails
                                            {
                                                RoomID = z.RoomID,
                                                RoomDiscount = z.RoomDiscount
                                            }).FirstOrDefault();
                    item.ddd = 5.0m;
                    var ddd = (from m in _context.MessageBoards
                               where m.HotelID == item.HotelID
                               select m.Score
                                          );
                    if (ddd.Count() != 0)
                    {
                        item.ddd = decimal.Round((decimal)ddd.Average(), 1, MidpointRounding.AwayFromZero);
                        int i = 0;
                    }
                }

                return images.ToList();
            }
            else
            {
                var images = (from p in _context.Hotels
                              orderby p.HotelID
                              select new CardViewModels
                              {
                                  HotelID = p.HotelID,
                                  HotelName = p.HotelName,
                                  HotelCity = p.HotelCity,
                                  Star = p.Star
                              }).Take(4).ToList();
                foreach (var item in images)
                {
                    item.ImageURL = (from x in _context.HotelImageUploads

                                     where x.HotelID == item.HotelID
                                     orderby x.ImageID
                                     select x.ImageURL).FirstOrDefault();
                    item.My_Rooms = (from d in _context.Rooms
                                     where d.HotelID == item.HotelID
                                     select new MyRoom
                                     {
                                         HotelID = d.HotelID,
                                         RoomPrice = d.RoomPrice
                                     }).FirstOrDefault();
                    string rid = (from d in _context.Rooms
                                  where d.HotelID == item.HotelID
                                  select d.RoomID).FirstOrDefault();
                    item.My_RoomsDetails = (from z in _context.RoomsDetails
                                            where z.RoomID == rid
                                            select new MyRoomsDetails
                                            {
                                                RoomID = z.RoomID,
                                                RoomDiscount = z.RoomDiscount
                                            }).FirstOrDefault();
                    item.ddd = 5.0m;
                    var ddd = (from m in _context.MessageBoards
                               where m.HotelID == item.HotelID
                               select m.Score
                                          );
                    if (ddd.Count() != 0)
                    {
                        item.ddd = decimal.Round((decimal)ddd.Average(), 1, MidpointRounding.AwayFromZero);
                        int i = 0;
                    }
                }

                return images.ToList();
            }         
        }
        public IEnumerable<MyHotels> GetHotelModels()
        {
            var hotel = from p in _context.Hotels
                        select new MyHotels
                        {
                            HotelID = p.HotelID,
                            HotelCity = p.HotelCity,
                            HotelName = p.HotelName,
                            HotelAddress = p.HotelAddress,
                            Star = p.Star
                        };
            return hotel.ToList();
        }
        public IEnumerable<MyHotels> GetCityModels()
        {
            var hotel = (from p in _context.Hotels                   
                         select new MyHotels
                        {                        
                            HotelCity = p.HotelCity
                         }).Distinct().Take(8);    
            return hotel;
        }
        public IEnumerable<MyHotels> GetModels()
        {
            var hotel = (from p in _context.Hotels
                         group p by p.HotelCity into g                                          
                         select new MyHotels
                         {                                
                             HotelCity = g.Key.ToString(),
                             Star = g.Count()
                         }).Take(8);
            return hotel;
        }

        public IEnumerable<Card> Getcards()
        {
            var productss = from p in cards
                            select new Card { name = p.name, ImageURL = p.ImageURL };
            return productss;
        }

    }
}