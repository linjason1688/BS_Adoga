using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;  //FormsAuthentication
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels;
using BS_Adoga.Models.ViewModels.HotelLogin;
using BS_Adoga.Models.ViewModels.MemberLogin;
using BS_Adoga.Service;
using Newtonsoft.Json;

namespace BS_Adoga.Controllers
{
    public class HotelLoginController : Controller
    {
        private AdogaContext _context;

        public HotelLoginController()
        {
            _context = new AdogaContext();
        }
        // GET: HotelLogin
        public ActionResult HotelLogin()
        {
            return View();
        }

        #region HotelLogin方法一(未驗證欄位)
        /*[HttpPost]
        public ActionResult HotelLogin(string Name,string Email,string Password1, string Password2)
        {
            HotelEmployee hotelEmployee = new HotelEmployee();
            if(Password1 != Password2)
            {
                return View();
            }

            else
            {
                hotelEmployee.HotelEmployeeID = Email;
                hotelEmployee.Name = Name;
                hotelEmployee.Email = Email;
                hotelEmployee.Password = Password1;
                hotelEmployee.RegisterDatetime = DateTime.Now;
                _context.HotelEmployees.Add(hotelEmployee);
                _context.SaveChanges();
                return RedirectToAction("HomePage", "Home");
            }

        }*/
        #endregion

        #region HotelLogin方法二(驗證欄位)
        [HttpPost]
        [MultiButton("register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(MixHotelLoginViewModel registerVM)
        {
            #region 未重構前的作法
            /*
            if (ModelState.IsValid)
            {
                //1.View Model(RegisterViewModel) --> Data Model (HotelEmployee)
                string name = HttpUtility.HtmlEncode(registerVM.RegisterViewModel.Name);
                string email = HttpUtility.HtmlEncode(registerVM.RegisterViewModel.Email);
                string password = HttpUtility.HtmlEncode(registerVM.RegisterViewModel.Password);

                HotelEmployee emp = new HotelEmployee()
                {
                    HotelEmployeeID = email,
                    Name = name,
                    Email = email,
                    MD5HashPassword = HashService.MD5Hash(password),
                    Logging = "建立" + ","+ name + "," + DateTime.Now.ToString(),
                    RegisterDatetime = DateTime.Now
                };

                //EF
                try
                {
                    _context.HotelEmployees.Add(emp);
                    _context.SaveChanges();

                    return Content("新增帳號成功");
                }
                catch (Exception ex)
                {
                    return Content("新增帳號失敗:" + ex.ToString());
                }
            }
            else return View(registerVM);
            */
            #endregion
            #region 重構後的作法
            if (ModelState.IsValid)
            {
                var service = new HotelEmployeeService();
                var result = service.Register(registerVM);
                if (result.IsSuccessful)
                {
                    return Content("新增帳號成功");
                }
                else
                {
                    var Log = result.WriteLog();
                    return Content("新增帳號失敗:" + Log);
                }
            }
            else
            {
                return View(registerVM);
            }
            #endregion
            
        }
        #endregion

        #region Login(登入)
        [HttpPost]
        [MultiButton("login")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(MixHotelLoginViewModel loginVM)
        {
            #region 未重構前的作法
            /*
            //一.未通過Model驗證
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            //二.通過Model驗證
            string email = HttpUtility.HtmlEncode(loginVM.HotelLoginView.Email);
            string password = HashService.MD5Hash(HttpUtility.HtmlEncode(loginVM.HotelLoginView.Password));

            //三.EF比對資料庫帳密
            //以Name及Password查詢比對Account資料表記錄

            HotelEmployee user = _context.HotelEmployees.Where(x => x.Email == email && x.MD5HashPassword == password).FirstOrDefault();

            //找不到則彈回Login頁
            if (user == null)
            {
                ModelState.AddModelError("NotFound", "無效的帳號或密碼!");

                return View(loginVM);
            }


            //四.FormsAuthentication Class -- https://docs.microsoft.com/zh-tw/dotnet/api/system.web.security.formsauthentication?view=netframework-4.8

            //FormsAuthenticationTicket Class
            //https://docs.microsoft.com/zh-tw/dotnet/api/system.web.security.formsauthenticationticket?view=netframework-4.8


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
            Response.Cookies.Add(cookie);

            //4.Redirect back to original URL.
            var url = FormsAuthentication.GetRedirectUrl(email, true);

            //5.Response.Redirect
            return RedirectToAction("Index", "Function");
            */
            #endregion

            #region 重構後的作法

            //一.未通過Model驗證
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            //二.通過Model驗證
            //三.EF比對資料庫帳密
            //以Name及Password查詢比對Account資料表記錄
            var service = new HotelEmployeeService();
            HotelEmployee user = service.CheckEmailPassword(loginVM);
            //找不到則彈回Login頁
            if (user == null)
            {
                ModelState.AddModelError("NotFound", "無效的帳號或密碼!");

                return View(loginVM);
            }

            //四.FormsAuthentication Class -- https://docs.microsoft.com/zh-tw/dotnet/api/system.web.security.formsauthentication?view=netframework-4.8
            //FormsAuthenticationTicket Class
            //https://docs.microsoft.com/zh-tw/dotnet/api/system.web.security.formsauthenticationticket?view=netframework-4.8
            Response.Cookies.Add(service.cookie(loginVM, user));

            //5.Response.Redirect
            return RedirectToAction("HotelIndex", "Function");
            
            #endregion
        }
        #endregion

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut(); //登出

            return RedirectToAction("HomePage", "Home");
        }

    }
}