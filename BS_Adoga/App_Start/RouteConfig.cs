using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BS_Adoga
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "HotelCity",
                url: "Taiwan/{city}",
                defaults: new { controller = "Search", action = "Search", city = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "SearchGetTempData",
            //    url: "Search/{search}",
            //    defaults: new { controller = "Search", action = "Search", city = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Login",
                url: "MemberLogin",
                defaults: new { controller = "MemberLogin", action = "MemberLogin", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "HotelDetail",
                url: "HotelDetail/{hotelName}-({startDate})-({endDate})-{orderRoom}-{adult}-{child}",
                defaults: new { controller = "HotelDetail", action = "HotelDetail"}
            );

            routes.MapRoute(
                name: "BookingOrderDetail",
                url: "BookingDetail/{orderid}",
                defaults: new { controller = "Account", action = "BookingDetail" }
            );

            routes.MapRoute(
                name: "PayAPI",
                url: "Account/RePayOrder/{orderid}",
                defaults: new { controller = "Account", action = "RePayOrder" }
            );

            routes.MapRoute(
                name: "HotelEdit",
                url: "Hotel/Edit/{hotelid}",
                defaults: new { controller = "Function", action = "HotelEdit", hotelid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "HotelBackEndDetail",
                url: "Hotel/Detail/{hotelid}",
                defaults: new { controller = "Function", action = "HotelDetails", hotelid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "HotelRoomsIndex",
                url: "Hotel/Room/{hotelid}",
                defaults: new { controller = "Function", action = "HotelRoomsIndex", hotelid = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "HotelRoomEdit",
                url: "Hotel/Room/Edit/{hotelids}-{roomid}",
                defaults: new { controller = "Function", action = "HotelRoomEdit", hotelids = UrlParameter.Optional , roomid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "RoomDetailExpansion",
                url: "Hotel/Room/Detail/Expansion/{year}-{month}-{roomid}",
                defaults: new { controller = "Function", action = "RoomDetailExpansion", year = UrlParameter.Optional, month = UrlParameter.Optional, roomid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "HomePage", id = UrlParameter.Optional }
            );
         
        }
    }
}
