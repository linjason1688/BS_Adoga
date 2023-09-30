using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BS_Adoga
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "GetAllRoom",
                routeTemplate: "api/HotelDetail/GetAllRoom",
                defaults: new { controller = "DetailApi", action = "GetAllRoom", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "GetSpecificRoom",
                routeTemplate: "api/HotelDetail/GetSpecificRoom",
                defaults: new { controller = "DetailApi", action = "GetSpecificRoom", id = RouteParameter.Optional }
            );

           config.Routes.MapHttpRoute(
                name: "GetHotelFacilities",
                routeTemplate: "api/HotelDetail/GetHotelFacilities",
                defaults: new { controller = "DetailApi", action = "GetHotelFacilities", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "GetRoomImages",
                routeTemplate: "api/HotelDetail/GetRoomImages",
                defaults: new { controller = "DetailApi", action = "GetRoomImages", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "GetHotelMessage",
                routeTemplate: "api/HotelDetail/GetHotelMessage",
                defaults: new { controller = "DetailApi", action = "GetHotelMessage", id = RouteParameter.Optional }
            );
            //config.Routes.MapHttpRoute(
            //    name: "GetEachHotelOneRoom",
            //    routeTemplate: "api/HotelDetail/GetEachHotelOneRoom",
            //    defaults: new { controller = "DetailApi", action = "GetEachHotelOneRoom", id = RouteParameter.Optional }
            //);

            //Irene
            // config.Routes.MapHttpRoute(
            //    name: "GetHotelFromCity",
            //    routeTemplate: "api/Search/GetHotelFromCity",
            //    defaults: new { controller = "Search", action = "GetHotelFromCity", id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
               name: "GetHotelByCity",
               routeTemplate: "api/Search/GetHotelByCity",
               defaults: new { controller = "SearchApi", action = "GetHotelByCity", id = RouteParameter.Optional }
           );


            config.Routes.MapHttpRoute(
                name: "UploadImage",
                routeTemplate: "api/Function/UploadImage",
                defaults: new { controller = "FunctionApi", action = "UploadImage", id = RouteParameter.Optional }
            );
            
            config.Routes.MapHttpRoute(
                name: "GetImageByID",
                routeTemplate: "api/Function/GetImageByID",
                defaults: new { controller = "FunctionApi", action = "GetImageByID", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "GetRoomOptionByID",
                routeTemplate: "api/Function/GetRoomOptionByID",
                defaults: new { controller = "FunctionApi", action = "GetRoomOptionByID", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "GetRoomDetailMonth",
                routeTemplate: "api/Function/RoomDetailMonth",
                defaults: new { controller = "FunctionApi", action = "GetRoomDetailMonth", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "EditRoomDetail",
                routeTemplate: "api/Function/EditRoomDetail",
                defaults: new { controller = "FunctionApi", action = "EditRoomDetail", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "GetOrderAllData",
                routeTemplate: "api/Function/OrderAllData",
                defaults: new { controller = "FunctionApi", action = "OrderAllDataBYEmpID", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
