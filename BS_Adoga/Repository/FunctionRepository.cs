using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelDetail;
using BS_Adoga.Models.ViewModels.HotelLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Repository
{
    public class FunctionRepository : DBRepository
    {
        private AdogaContext _context;

        
        public FunctionRepository(AdogaContext context) : base(context)
        {
            _context = new AdogaContext();
        }

        /// <summary>
        /// 取得所有Hotel的資料到HotelListVM
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HotelListViewModel> GetHotelList()
        {
            var table = (from h in _context.Hotels
                         select new HotelListViewModel
                         {
                             HotelID = h.HotelID,
                             HotelName = h.HotelName,
                             HotelEngName = h.HotelEngName,
                             HotelCity = h.HotelCity,
                             HotelDistrict = h.HotelDistrict,
                             HotelAddress = h.HotelAddress
                         });
            return table;
        }

        /// <summary>
        /// 取得登入的使用者對應到的所有Hotel資料
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HotelListViewModel> GetHotelListByEmpID(string EmpID)
        {
            var table = (from h in _context.Hotels
                         join EmpMapHotel in _context.HotelEmpMappingHotels on h.HotelID equals EmpMapHotel.HotelID
                         where EmpMapHotel.HotelEmployeeID == EmpID
                         select new HotelListViewModel
                         {
                             HotelID = h.HotelID,
                             HotelName = h.HotelName,
                             HotelEngName = h.HotelEngName,
                             HotelCity = h.HotelCity,
                             HotelDistrict = h.HotelDistrict,
                             HotelAddress = h.HotelAddress
                         });
            return table;
        }

        /// <summary>
        /// 取得所有Hotel飯店設施 By使用者ID
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Facility> GetHotelFaciliyByEmpID(string EmpID)
        {
            var facility = (from f in _context.Facilities
                            join EmpMapHotel in _context.HotelEmpMappingHotels on f.HotelID equals EmpMapHotel.HotelID
                            where EmpMapHotel.HotelEmployeeID == EmpID
                            select f);
            return facility;
        }

        /// <summary>
        /// 取得所有Hotel有幾筆Room房型
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HotelRoomViewModel> GetHotelRoomCount()
        {
            var table = (from h in _context.Hotels
                         select new HotelRoomViewModel
                         {
                             HotelID = h.HotelID,
                             HotelName = h.HotelName,
                             RoomCount = (from r in _context.Rooms
                                          where r.HotelID == h.HotelID
                                          select r.RoomID).Count()
                         });
            return table;
        }

        /// <summary>
        /// 取得所有Hotel有幾筆Room房型 By使用者ID
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public IEnumerable<HotelRoomViewModel> GetHotelRoomCountByEmpID(string EmpID)
        {
            var table = (from h in _context.Hotels
                         join EmpMapHotel in _context.HotelEmpMappingHotels on h.HotelID equals EmpMapHotel.HotelID
                         where EmpMapHotel.HotelEmployeeID == EmpID
                         select new HotelRoomViewModel
                         {
                             HotelID = h.HotelID,
                             HotelName = h.HotelName,
                             RoomCount = (from r in _context.Rooms
                                          where r.HotelID == h.HotelID
                                          select r.RoomID).Count()
                         });
            return table;
        }

        /// <summary>
        /// 取得所有房型資料 BY hotelid 
        /// </summary>
        /// <param name="hotelid"></param>
        /// <returns></returns>
        public IEnumerable<Room> GetHotelRoomAll(string hotelid)
        {
            var table =  from r in _context.Rooms
                         where r.HotelID == hotelid
                         select r;
            return table;
        }

        /// <summary>
        /// 取得當月所有的RoomDetail資料
        /// </summary>
        /// <param name="roomid"></param>
        /// <returns></returns>
        public IEnumerable<RoomsDetail> GetAllRoomDetailThisMonth(string roomid)
        {
            string YearMonth = DateTime.Now.ToString("yyyy/M");
            string ThisMonthFirstDay = $"{YearMonth}/1 00:00:00";

            DateTime ThisMonthFirstDayObject = Convert.ToDateTime(ThisMonthFirstDay);
            DateTime NextMonthFirstDayObject = ThisMonthFirstDayObject.AddMonths(1);

            var table = from rd in _context.RoomsDetails
                        where rd.RoomID == roomid && rd.CheckInDate >= ThisMonthFirstDayObject && rd.CheckInDate < NextMonthFirstDayObject
                        orderby rd.CheckInDate ascending
                        select rd;
            return table;
        }

        /// <summary>
        /// 取得一個月所有的房型的資料
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="roomid"></param>
        /// <returns></returns>
        public IEnumerable<RoomsDetail> GetAllRoomDetailMonth(string year, string month, string roomid)
        {
            string MonthFirstDay = $"{year}/{month}/1 00:00:00";

            DateTime MonthFirstDayObject = Convert.ToDateTime(MonthFirstDay);
            DateTime NextMonthFirstDayObject = MonthFirstDayObject.AddMonths(1);

            var table = from rd in _context.RoomsDetails
                        where rd.RoomID == roomid && rd.CheckInDate >= MonthFirstDayObject && rd.CheckInDate < NextMonthFirstDayObject
                        orderby rd.CheckInDate ascending
                        select rd;
            return table;
        }

        /// <summary>
        /// 顯示房型有的床型
        /// </summary>
        /// <param name="roomid"></param>
        /// <returns></returns>
        public IEnumerable<RoomBedVM> GetRoomBeds(string roomid)
        {

            var RoomBeds = ((from rb in _context.RoomBeds
                        join bt in _context.BedTypes on rb.TypesOfBedsID equals bt.TypesOfBedsID
                        where rb.RoomID == roomid
                        select new RoomBedVM
                        {
                            Name = bt.Name,
                            Amount = rb.Amount
                        }));
            return RoomBeds;
        }

        /// <summary>
        /// 取得所有該員工有的飯店訂單
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public IEnumerable<OrderViewModel> GetAllOrderByEmpID(string EmpID)
        {
            var table = from o in _context.Orders
                        join r in _context.Rooms on o.RoomID equals r.RoomID
                        join h in _context.Hotels on r.HotelID equals h.HotelID
                        join EmpMapHotel in _context.HotelEmpMappingHotels on h.HotelID equals EmpMapHotel.HotelID
                        where EmpMapHotel.HotelEmployeeID == EmpID
                        orderby o.RoomID ascending, o.CheckInDate ascending
                        select new OrderViewModel
                        {
                            OrderID = o.OrderID,
                            CustomerID = o.CustomerID,
                            RoomID = o.RoomID,
                            RoomName = r.RoomName,
                            OrderDate = o.OrderDate,
                            CheckInDate = o.CheckInDate,
                            CheckOutDate = o.CheckOutDate,
                            RoomCount = o.RoomCount,
                            RoomPriceTotal = o.RoomPriceTotal,
                            FirstName = o.FirstName,
                            LastName = o.LastName,
                            Email = o.Email,
                            PhoneNumber = o.PhoneNumber,
                            Country = o.Country,
                            SmokingPreference = o.SmokingPreference,
                            BedPreference = o.BedPreference,
                            ArrivingTime = o.ArrivingTime,
                            PaymentStatus = o.PaymentStatus
                        };
            return table;
        }


        /// <summary>
        /// 顯示所有飯店未入住訂單 BY 員工編號
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public IEnumerable<OrderViewModel> GetOrderNotCheckIn(string EmpID)
        {
            string Today = DateTime.Now.ToString("yyyy/MM/dd");
            DateTime TodayObject = Convert.ToDateTime(Today);

            var table = from o in _context.Orders
                        join r in _context.Rooms on o.RoomID equals r.RoomID
                        join h in _context.Hotels on r.HotelID equals h.HotelID
                        join EmpMapHotel in _context.HotelEmpMappingHotels on h.HotelID equals EmpMapHotel.HotelID
                        where EmpMapHotel.HotelEmployeeID == EmpID && o.CheckInDate >= TodayObject
                        orderby o.RoomID ascending, o.CheckInDate ascending
                        select new OrderViewModel
                        {
                            OrderID = o.OrderID,
                            CustomerID = o.CustomerID,
                            RoomID = o.RoomID,
                            RoomName = r.RoomName,
                            OrderDate = o.OrderDate,
                            CheckInDate = o.CheckInDate,
                            CheckOutDate = o.CheckOutDate,
                            RoomCount = o.RoomCount,
                            RoomPriceTotal = o.RoomPriceTotal,
                            FirstName = o.FirstName,
                            LastName = o.LastName,
                            Email = o.Email,
                            PhoneNumber = o.PhoneNumber,
                            Country = o.Country,
                            SmokingPreference = o.SmokingPreference,
                            BedPreference = o.BedPreference,
                            ArrivingTime = o.ArrivingTime,
                            PaymentStatus = o.PaymentStatus
                        };
            return table;
        }

        /// <summary>
        /// 顯示所有飯店已入住訂單 BY 員工編號
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public IEnumerable<OrderViewModel> GetOrderCheckIn(string EmpID)
        {
            string Today = DateTime.Now.ToString("yyyy/MM/dd");
            DateTime TodayObject = Convert.ToDateTime(Today);

            var table = from o in _context.Orders
                        join r in _context.Rooms on o.RoomID equals r.RoomID
                        join h in _context.Hotels on r.HotelID equals h.HotelID
                        join EmpMapHotel in _context.HotelEmpMappingHotels on h.HotelID equals EmpMapHotel.HotelID
                        where EmpMapHotel.HotelEmployeeID == EmpID && o.CheckInDate < TodayObject
                        orderby o.RoomID ascending, o.CheckInDate ascending
                        select new OrderViewModel
                        {
                            OrderID = o.OrderID,
                            CustomerID = o.CustomerID,
                            RoomID = o.RoomID,
                            RoomName = r.RoomName,
                            OrderDate = o.OrderDate,
                            CheckInDate = o.CheckInDate,
                            CheckOutDate = o.CheckOutDate,
                            RoomCount = o.RoomCount,
                            RoomPriceTotal = o.RoomPriceTotal,
                            FirstName = o.FirstName,
                            LastName = o.LastName,
                            Email = o.Email,
                            PhoneNumber = o.PhoneNumber,
                            Country = o.Country,
                            SmokingPreference = o.SmokingPreference,
                            BedPreference = o.BedPreference,
                            ArrivingTime = o.ArrivingTime,
                            PaymentStatus = o.PaymentStatus
                        };
            return table;
        }

        public IEnumerable<OrderViewModel> GetOrderNotPay(string EmpID)
        {
            string Today = DateTime.Now.ToString("yyyy/MM/dd");
            DateTime TodayObject = Convert.ToDateTime(Today);

            var table = from o in _context.Orders
                        join r in _context.Rooms on o.RoomID equals r.RoomID
                        join h in _context.Hotels on r.HotelID equals h.HotelID
                        join EmpMapHotel in _context.HotelEmpMappingHotels on h.HotelID equals EmpMapHotel.HotelID
                        where EmpMapHotel.HotelEmployeeID == EmpID && o.CheckInDate >= TodayObject && o.PaymentStatus == false
                        orderby o.RoomID ascending, o.CheckInDate ascending
                        select new OrderViewModel
                        {
                            OrderID = o.OrderID,
                            CustomerID = o.CustomerID,
                            RoomID = o.RoomID,
                            RoomName = r.RoomName,
                            OrderDate = o.OrderDate,
                            CheckInDate = o.CheckInDate,
                            CheckOutDate = o.CheckOutDate,
                            RoomCount = o.RoomCount,
                            RoomPriceTotal = o.RoomPriceTotal,
                            FirstName = o.FirstName,
                            LastName = o.LastName,
                            Email = o.Email,
                            PhoneNumber = o.PhoneNumber,
                            Country = o.Country,
                            SmokingPreference = o.SmokingPreference,
                            BedPreference = o.BedPreference,
                            ArrivingTime = o.ArrivingTime,
                            PaymentStatus = o.PaymentStatus
                        };
            return table;
        }

    }
    
}