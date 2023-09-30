using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Service;
using System.Web.Security;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.MemberLogin;
using Newtonsoft.Json;
using BS_Adoga.Repository;
using BS_Adoga.Models.ViewModels.Account;

namespace BS_Adoga.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private AdogaContext _context;
        private MemberAccountService _service;
        private MemberAccountRepository _memberacoountrepository;

        public AccountController()
        {
            _context = new AdogaContext();
            _service = new MemberAccountService();
            _memberacoountrepository = new MemberAccountRepository();
        }

        [AcceptVerbs("GET")]
        public ActionResult GetMemberBookingList(string filterOption, string sortOption, string UserInputOrderId)
        {
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            var data = _service.GetBookingOrder_FilterSort(user_id, filterOption, sortOption, UserInputOrderId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Account
        public ActionResult MemberBooking()
        {
            ViewBag.MemberCurrentPage = "booking";
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            return View(_memberacoountrepository.GetBookingDESC(user_id));
        }
        public ActionResult MemberProfile()
        {
            //string member = User.Identity.Name;
            //var data = _service.GetMember(member);
            ViewBag.MemberCurrentPage = "profile";
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;
            AdogaContext db = new AdogaContext();
            //string user_id = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            //var data = from m in db.Customers
            //           where m.CustomerID == user_id
            //           select m;

            var data = db.Customers.Where(x => x.CustomerID == user_id).FirstOrDefault();
            return View(data);



            //使用表單驗證來進行使用者身份識別，當Http的連線字串等於使用者的真確性，回傳表單驗證的使用者特定資料(使用者特定字串)
            //字串 id = ((使用表單驗證來進行使用者的身份識別) Http連接字串.使用者.身份識別).取得表單驗證的使用者身份識別.取得存放使用者的特定字串
            //string id = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;

            //return View();
        }
        [HttpPost]
        public ActionResult MemberProfilePassword(string CheckPassword, string NewPassword, string ConfirmPassword, string Email)
        {
            //string FirstPassword = HttpUtility.HtmlEncode(CheckPassword);
            //string SecendPassword = HttpUtility.HtmlEncode(NewPassword);
            //string ThirdPassword = HttpUtility.HtmlEncode(ConfirmPassword);
            Customer cust = new Customer();
            cust.Email = Email;
            cust.MD5HashPassword = HashService.MD5Hash(NewPassword);
            AdogaContext db = new AdogaContext();
            var data = db.Customers.Find(Email);
            if (data.MD5HashPassword != HashService.MD5Hash(CheckPassword))
            {
                Content("修改失敗");
                TempData["Error"] = "原密碼輸入錯誤,請重新輸入";
            }
            else
            {
                if (NewPassword != ConfirmPassword)
                {

                    Content("修改失敗");
                    TempData["Error"] = "輸入密碼與再次輸入密碼不相等請重新輸入請重新輸入";
                }
                else
                {
                    data.MD5HashPassword = HashService.MD5Hash(NewPassword);
                    TempData["Success"] = "密碼修改成功";
                    db.SaveChanges();

                }
            }

            return RedirectToAction("MemberProfile", "Account");
        }


        public ActionResult BookingDetail(string orderid)
        {
            ViewBag.MemberCurrentPage = "booking";

            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;


            return View(_memberacoountrepository.GetBookingDetail(orderid, user_id));
        }

        [HttpGet]
        public ActionResult RePayOrder(RePayViewModel rePay, string orderid)
        {
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            var reOdId = _memberacoountrepository.GetReOrder(orderid, user_id);
            rePay.OrderID = orderid;
            rePay.HotelName = reOdId.HotelName;
            rePay.RoomPriceTotal = reOdId.RoomPriceTotal;
            rePay.RoomQuantity = reOdId.RoomQuantity;


            TempData["ReOrderData"] = rePay;

            if (ModelState.IsValid)
            {
                //EF
                try
                {
                    return RedirectToAction("PayAPI", "CheckOut");
                }
                catch (Exception ex)
                {
                    return Content("訂單建立失敗:" + ex.ToString());
                }
            }
            return View();
        }

        public ActionResult MemberProfileName(string InputFirstName, string InputLastName, string Email)
        {
            Customer cust = new Customer();
            cust.Email = Email;
            AdogaContext db = new AdogaContext();
            var data = db.Customers.Find(Email);

            data.FirstName = InputFirstName;
            data.LastName = InputLastName;
            db.SaveChanges();




            return RedirectToAction("MemberProfile", "Account");
        }

        public ActionResult MemberProfilePhone(string PhoneNumber, string Email)
        {

            Customer cust = new Customer();
            cust.Email = Email;
            AdogaContext db = new AdogaContext();
            var data = db.Customers.Find( Email);
            data.PhoneNumber = PhoneNumber;
            db.SaveChanges();

            return RedirectToAction("MemberProfile", "Account");
        }

        [HttpPost]
        public ActionResult Evaluation(string orderid, decimal ScoreRange, string Title, string MessageText)
        {
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;


            var getId = _memberacoountrepository.GetComment(orderid, user_id);


            AdogaContext db = new AdogaContext();
            var selOrderId = db.MessageBoards.Any(x => x.OrderID == orderid);

            if (selOrderId)
            {
                TempData["message"] = "已經評論過";
                return RedirectToAction("EvaluationPage");
            }
            else
            {
                MessageBoard message = new MessageBoard()
                {
                    OrderID = orderid,
                    HotelID = getId.HotelID,
                    CustomerID = user_id,
                    Title = Title,
                    MessageDate = DateTime.Now,
                    MessageText = MessageText,
                    Score = ScoreRange
                };

                db.MessageBoards.Add(message);
                db.SaveChanges();
            }

            return RedirectToAction("MemberBooking");
        }

        public ActionResult GetEvaluationPage()
        {
            AdogaContext db = new AdogaContext();

            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            var test = _memberacoountrepository.GetEvaluationPage(user_id);
            

            return Json(test, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EvaluationPage()
        {
            ViewBag.MemberCurrentPage = "evaluation";
            AdogaContext db = new AdogaContext();

            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            var test = _memberacoountrepository.GetEvaluationPage(user_id);


            return View();
        }
    }
}