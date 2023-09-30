using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels;
using BS_Adoga.Models.ViewModels.Account;
using BS_Adoga.Models.ViewModels.HotelDetail;

namespace BS_Adoga.Repository
{
    public class MemberAccountRepository
    {
        private AdogaContext _context;

        public MemberAccountRepository()
        {
            _context = new AdogaContext();
        }

        public IQueryable<Customer> GetMember(string id)
        {
            var memberData = from c in _context.Customers
                             where c.CustomerID == id
                             select c;

            return memberData;
        }

        public IEnumerable<MemberBookingViewModel> GetBookingDESC(string customerID)
        {
            var table = (from o in _context.Orders
                          join r in _context.Rooms on o.RoomID equals r.RoomID
                          join h in _context.Hotels on r.HotelID equals h.HotelID
                          where o.CustomerID == customerID
                          orderby o.OrderID descending
                          select new MemberBookingViewModel 
                          {
                              OrderID = o.OrderID,
                              HotelID = h.HotelID,
                              HotelName = h.HotelName,
                              HotelEngName = h.HotelEngName,
                              HotelImageURL = (from m in _context.HotelImageUploads
                                               where h.HotelID == m.HotelID
                                               orderby m.ImageURL
                                               select m.ImageURL).FirstOrDefault(), 
                              RoomBed = ((from rb in _context.RoomBeds
                                          join bt in _context.BedTypes on rb.TypesOfBedsID equals bt.TypesOfBedsID
                                          where rb.RoomID == o.RoomID
                                          select new RoomBedVM
                                          {
                                              Name = bt.Name,
                                              Amount = rb.Amount
                                          })),
                              RoomPriceTotal = o.RoomPriceTotal,
                              OrderDate = o.OrderDate,
                              CheckInDate = o.CheckInDate,
                              CheckOutDate = o.CheckOutDate,
                              Breakfast = r.Breakfast,
                              City = h.HotelCity,
                              PayStatus = o.PaymentStatus
                          });
            return table;
        }

        
        public BookingDetailViewModel GetBookingDetail(string orderID,string customerID)
        {
            var table = (from o in _context.Orders
                         join r in _context.Rooms on o.RoomID equals r.RoomID
                         join h in _context.Hotels on r.HotelID equals h.HotelID
                         where o.CustomerID == customerID && o.OrderID == orderID
                         orderby o.OrderID descending
                         select new BookingDetailViewModel
                         {
                             HotelID = h.HotelID,
                             HotelName = h.HotelName,
                             HotelEngName = h.HotelEngName,
                             HotelImageURL = (from m in _context.HotelImageUploads
                                              where h.HotelID == m.HotelID
                                              orderby m.ImageURL
                                              select m.ImageURL).FirstOrDefault(),
                             Star = h.Star,
                             HotelCity = h.HotelCity,
                             HotelDistrict = h.HotelDistrict,
                             HotelAddress = h.HotelAddress,
                             OrderID = o.OrderID,
                             OrderDate = o.OrderDate,
                             CheckInDate = o.CheckInDate,
                             CheckOutDate = o.CheckOutDate,
                             RoomName = r.RoomName,
                             RoomCount = o.RoomCount,
                             Email = o.Email,
                             Name = o.FirstName + " " +o.LastName,
                             PhoneNumber = o.PhoneNumber,
                             RoomBed = ((from rb in _context.RoomBeds
                                         join bt in _context.BedTypes on rb.TypesOfBedsID equals bt.TypesOfBedsID
                                         where rb.RoomID == o.RoomID
                                         select new RoomBedVM
                                         {
                                             Name = bt.Name,
                                             Amount = rb.Amount
                                         })),
                             RoomPriceTotal = o.RoomPriceTotal,
                             PayStatus = o.PaymentStatus
                         }).FirstOrDefault();
            return table;
        }

        
        public RePayViewModel GetReOrder(string orderID, string customerID)
        {
            var table = (from o in _context.Orders
                         join r in _context.Rooms on o.RoomID equals r.RoomID
                         join h in _context.Hotels on r.HotelID equals h.HotelID
                         where o.CustomerID == customerID && o.OrderID == orderID
                         orderby o.OrderID descending
                         select new RePayViewModel
                         {
                             HotelID = h.HotelID,
                             HotelName = h.HotelName,
                             OrderID = o.OrderID,
                             RoomQuantity = o.RoomCount,
                             RoomPriceTotal = o.RoomPriceTotal,
                             PayStatus = o.PaymentStatus
                         }).FirstOrDefault();
            return table;
        }
        public IEnumerable<MemberBookingViewModel> GetBookingOrderByID(string UserInputOrderId)
        {
            //var OrderIdSearchResult = _context.Orders.Where(x => x.OrderID.Contains(UserInputOrderId.ToString()));

            var SearchResultByOrderID = (from o in _context.Orders
                                         join r in _context.Rooms on o.RoomID equals r.RoomID
                                         join h in _context.Hotels on r.HotelID equals h.HotelID
                                         where o.OrderID.Contains(UserInputOrderId.ToString())
                                         select new MemberBookingViewModel
                                         {
                                             OrderID = o.OrderID,
                                             HotelID = h.HotelID,
                                             HotelName = h.HotelName,
                                             HotelEngName = h.HotelEngName,
                                             RoomBed = ((from rb in _context.RoomBeds
                                                         join bt in _context.BedTypes on rb.TypesOfBedsID equals bt.TypesOfBedsID
                                                         where rb.RoomID == o.RoomID
                                                         select new RoomBedVM
                                                         {
                                                             Name = bt.Name,
                                                             Amount = rb.Amount
                                                         })),
                                             RoomPriceTotal = o.RoomPriceTotal,
                                             OrderDate = o.OrderDate,
                                             CheckInDate = o.CheckInDate,
                                             CheckOutDate = o.CheckOutDate,
                                             Breakfast = r.Breakfast,
                                             City = h.HotelCity,
                                             PayStatus = o.PaymentStatus
                                         }
                    );
    

            return SearchResultByOrderID;
        }

        public CommentViewModel GetComment(string orderID, string customerID)
        {
            var table = (from o in _context.Orders
                         join r in _context.Rooms on o.RoomID equals r.RoomID
                         join h in _context.Hotels on r.HotelID equals h.HotelID
                         where o.CustomerID == customerID && o.OrderID == orderID
                         orderby o.OrderID descending
                         select new CommentViewModel
                         {
                             HotelID = h.HotelID,
                             HotelName = h.HotelName,
                             OrderID = o.OrderID
                         }).FirstOrDefault();
            return table;
        }

        public IEnumerable<EvaluationPageViewModel> GetEvaluationPage(string customerID)
        {

            var table = (from m in _context.MessageBoards.AsEnumerable()
                         join h in _context.Hotels on m.HotelID equals h.HotelID
                         join o in _context.Orders on m.OrderID equals o.OrderID
                         join r in _context.Rooms on o.RoomID equals r.RoomID
                         where m.CustomerID == customerID && m.OrderID == o.OrderID
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