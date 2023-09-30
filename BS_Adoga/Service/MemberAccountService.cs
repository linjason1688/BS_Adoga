using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Repository;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels;
using BS_Adoga.Models.ViewModels.Account;

namespace BS_Adoga.Service
{
    public class MemberAccountService
    {
        private MemberAccountRepository _repository;

        public MemberAccountService()
        {
            _repository = new MemberAccountRepository();
        }

        public IQueryable<Customer> GetMember(string id)
        {
           return  _repository.GetMember(id);
        }

        public BookingDetailViewModel GetBookingDetail(string orderID, string customerID)
        {
            return _repository.GetBookingDetail(orderID,customerID);
        }
        //public BookingOrderViewModel GetBookingOrderByID(string UserInputOrderId)
        //{
        //    var data = _repository.GetBookingOrderByID(UserInputOrderId);

        //    var result = from item in data
        //                 select new BookingOrderViewModel
        //                 {
        //                     OrderID = item.OrderID,
        //                     HotelID = item.HotelID,
        //                     HotelName = item.HotelName,
        //                     HotelEngName = item.HotelEngName,
        //                     RoomBed = item.RoomBed,
        //                     RoomPriceTotal = item.RoomPriceTotal,

        //                     OrderDate = item.OrderDate,
        //                     CheckInDate = item.CheckInDate,
        //                     CheckOutDate = item.CheckOutDate,

        //                     FewDaysAgo = new TimeSpan(DateTime.Now.Ticks - item.OrderDate.Ticks).Days,

        //                     OrderDateStr = item.OrderDate.ToString("yyyy年MM月dd日"),

        //                     CheckInDay = item.CheckInDate.ToString("dd"),
        //                     CheckInWeek = item.CheckInDate.ToString("MMM"),
        //                     CheckInMonth = item.CheckInDate.ToString("ddd"),

        //                     CheckOutDay = item.CheckOutDate.ToString("dd"),
        //                     CheckOutWeek = item.CheckOutDate.ToString("MMM"),
        //                     CheckOutMonth = item.CheckOutDate.ToString("ddd"),

        //                     CheckCheckOut = item.CheckOutDate.CompareTo(DateTime.Now),
        //                     City = item.City,
        //                     Breakfast = item.Breakfast,
        //                     PayStatus = item.PayStatus,
        //                     In24Hours = DateTime.Now.Subtract(item.OrderDate) < TimeSpan.FromHours(24)
        //                 };

        //    //return _repository.GetBookingOrderByID(UserInputOrderId);
        //}

        //public IEnumerable<BookingOrderViewModel> GetBookingOrderDESC (string customerID)
        //{
        //    var data = _repository.GetBookingDESC(customerID);

        //    var result = from item in data
        //                 select new BookingOrderViewModel
        //                 {
        //                     OrderID = item.OrderID,
        //                     HotelID = item.HotelID,
        //                     HotelName = item.HotelName,
        //                     HotelEngName = item.HotelEngName,
        //                     RoomBed = item.RoomBed,
        //                     RoomPriceTotal = item.RoomPriceTotal,

        //                     OrderDate = item.OrderDate,
        //                     CheckInDate = item.CheckInDate,
        //                     CheckOutDate = item.CheckOutDate,

        //                     FewDaysAgo = new TimeSpan(DateTime.Now.Ticks - item.OrderDate.Ticks).Days,

        //                     OrderDateStr = item.OrderDate.ToString("yyyy年MM月dd日"),

        //                     CheckInDay = item.CheckInDate.ToString("dd"),
        //                     CheckInWeek = item.CheckInDate.ToString("MMM"),
        //                     CheckInMonth = item.CheckInDate.ToString("ddd"),

        //                     CheckOutDay = item.CheckOutDate.ToString("dd"),
        //                     CheckOutWeek = item.CheckOutDate.ToString("MMM"),
        //                     CheckOutMonth = item.CheckOutDate.ToString("ddd"),

        //                     CheckCheckOut = item.CheckOutDate.CompareTo(DateTime.Now),
        //                     City = item.City,
        //                     Breakfast = item.Breakfast,
        //                     PaymentStatus = item.PaymentStatus
        //                 };

        //    return result;
        //}

        public IEnumerable<BookingOrderViewModel> GetBookingOrder_FilterSort(string user_id, string filterOption, string sortOption,string UserInputOrderId)
        {
            IEnumerable<BookingOrderViewModel> result = null;

            //先根據付款狀態篩選資料
            if(filterOption == "ComingSoon")
            {
                var data = GetBookingOrder_FilterPayment(user_id, true, UserInputOrderId);
                result = from d in data
                         where d.CheckInDate > DateTime.Now
                         select d;
            }
            else if (filterOption == "Complete")
            {
                var data = GetBookingOrder_FilterPayment(user_id, true, UserInputOrderId);
                result = from d in data
                         where d.CheckOutDate < DateTime.Now
                         select d;
            }
            else
            {
                result = GetBookingOrder_FilterPayment(user_id, false , UserInputOrderId);
            }

            //再根據OrderDate或者CheckInDate來做desc的排序。
            if(sortOption == "OrderDate")
                return result.OrderByDescending(x=>x.OrderDate).AsEnumerable();            
            else
                return result.OrderByDescending(x => x.CheckInDate).AsEnumerable();                             
            
        }

        public IEnumerable<BookingOrderViewModel> GetBookingOrder_FilterPayment(string customerID,bool IsPay,string UserInputOrderId)
        {
            var data = _repository.GetBookingDESC(customerID);

            var result = from item in data
                         where item.PayStatus == IsPay && item.OrderID.Contains(UserInputOrderId)
                         select new BookingOrderViewModel
                         {
                             OrderID = item.OrderID,
                             HotelID = item.HotelID,
                             HotelName = item.HotelName,
                             HotelEngName = item.HotelEngName,
                             HotelImageURL = item.HotelImageURL,
                             RoomBed = item.RoomBed,
                             RoomPriceTotal = item.RoomPriceTotal,

                             OrderDate = item.OrderDate,
                             CheckInDate = item.CheckInDate,
                             CheckOutDate = item.CheckOutDate,

                             FewDaysAgo = new TimeSpan(DateTime.Now.Ticks - item.OrderDate.Ticks).Days,

                             OrderDateStr = item.OrderDate.ToString("yyyy年MM月dd日"),

                             CheckInDay = item.CheckInDate.ToString("dd"),
                             CheckInWeek = item.CheckInDate.ToString("MMM"),
                             CheckInMonth = item.CheckInDate.ToString("ddd"),

                             CheckOutDay = item.CheckOutDate.ToString("dd"),
                             CheckOutWeek = item.CheckOutDate.ToString("MMM"),
                             CheckOutMonth = item.CheckOutDate.ToString("ddd"),

                             CheckCheckOut = item.CheckOutDate.CompareTo(DateTime.Now),
                             City = item.City,
                             Breakfast = item.Breakfast,
                             PayStatus = item.PayStatus,
                             In24Hours = DateTime.Now.Subtract(item.OrderDate) < TimeSpan.FromHours(24)
                         };

            return result;
        }
    }
}