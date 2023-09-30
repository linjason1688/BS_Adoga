using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelLogin;
using BS_Adoga.Models.ViewModels.MemberLogin;
using BS_Adoga.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BS_Adoga.Service
{
    public class HotelEmployeeService
    {
        /// <summary>
        /// 註冊飯店員工帳號
        /// </summary>
        /// <param name="registerVM"></param>
        /// <returns></returns>
        public OperationResult Register(MixHotelLoginViewModel registerVM)
        {
            var result = new OperationResult();
            try
            {
                //1.View Model(RegisterViewModel) --> Data Model (HotelEmployee)
                var repository = new DBRepository(new AdogaContext());
                HotelEmployee entity = new HotelEmployee()
                {
                    HotelEmployeeID = registerVM.RegisterViewModel.Email,
                    Name = registerVM.RegisterViewModel.Name,
                    Email = registerVM.RegisterViewModel.Email,
                    MD5HashPassword = HashService.MD5Hash(registerVM.RegisterViewModel.Password),
                    Logging = "建立" + "," + registerVM.RegisterViewModel.Name + "," + DateTime.Now.ToString(),
                    RegisterDatetime = DateTime.Now
                };
                repository.Create(entity);
                repository.SaveChanges();
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Exception = ex;
            }
            return result;
        }

        /// <summary>
        /// 比對帳密是否存在
        /// </summary>
        /// <param name="loginVM"></param>
        /// <returns></returns>
        public HotelEmployee CheckEmailPassword(MixHotelLoginViewModel loginVM)
        {
            string email = HttpUtility.HtmlEncode(loginVM.HotelLoginView.Email);
            string password = HashService.MD5Hash(HttpUtility.HtmlEncode(loginVM.HotelLoginView.Password));

            //三.EF比對資料庫帳密
            //以Name及Password查詢比對Account資料表記錄
            var repository = new DBRepository(new AdogaContext());
            HotelEmployee user = repository.GetAll<HotelEmployee>().Where(x => x.Email == email && x.MD5HashPassword == password).FirstOrDefault();          //_context.HotelEmployees.Where(x => x.Email == email && x.MD5HashPassword == password).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// 產生cookie
        /// </summary>
        /// <param name="loginVM"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public HttpCookie cookie(MixHotelLoginViewModel loginVM,HotelEmployee user)
        {
            UserCookieViewModel UserData = new UserCookieViewModel()
            {
                Id = user.Email,
                PictureUrl = string.Empty
            };
            //1.Create FormsAuthenticationTicket
            var ticket = new FormsAuthenticationTicket(
            version: 1,
            name: user.Name.ToString(), //可以放使用者Id
            issueDate: DateTime.UtcNow,//現在UTC時間
            expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
            isPersistent: loginVM.HotelLoginView.Remember,// 是否要記住我 true or false
            userData: JsonConvert.SerializeObject(UserData), //可以放使用者角色名稱
            cookiePath: FormsAuthentication.FormsCookiePath);

            //2.Encrypt the Ticket
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            //3.Create the cookie.
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            return(cookie);

            //4.Redirect back to original URL.
            //var url = FormsAuthentication.GetRedirectUrl(user.Email, true);
        }
    }
}