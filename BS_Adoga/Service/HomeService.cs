using BS_Adoga.Models.ViewModels.homeViewModels;
using BS_Adoga.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Service
{
    public class HomeService
    {
        public HomeRepository _homeRepository;
        public HomeService()
        {
            _homeRepository = new HomeRepository();
        }
        //public demoshopViewModels GetHomeByFilter()
        //{
        //    var result = _homeRepository.Getcards();
        //    return result;
        //}
        public demoshopViewModels ALLImages(string cardlocal)
        {

            var productss = new demoshopViewModels()
            {
                My_MyHotels = _homeRepository.GetHotelModels(),
                My_CardViewModels = _homeRepository.GetCardModels(cardlocal),
                My_city= _homeRepository.GetCardModels4(cardlocal)
            };

            return productss;
        }
            public demoshopViewModels ALLImages2()
            {

                var productss = new demoshopViewModels()
                {
                    My_MyHotels = _homeRepository.GetHotelModels(),
                    My_CardViewModels = _homeRepository.GetCardModels2(),
                    My_CardViewModels2 = _homeRepository.GetCardModels3(),
                    My_MyCitys = _homeRepository.GetCityModels(),
                    My_Mys = _homeRepository.GetModels(),
                    Cards = _homeRepository.Getcards()
                };

                return productss;
            }

    }
}