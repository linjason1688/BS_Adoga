using BS_Adoga.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.ViewModels.MemberLogin;
using BS_Adoga.Service;
using System.Web.Security;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Web.Helpers;

namespace BS_Adoga.Controllers
{
    public class MemberLoginController : Controller
    {
        private AdogaContext _context;
        public MemberLoginController()
        {
            _context = new AdogaContext();
        }
        // GET: MemberLogin
        public ActionResult MemberLogin()
        {
            return View();
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut(); //登出

            return RedirectToAction("HomePage", "Home");
        }
        #region memberlogin方法一未驗證欄位
        //[HttpPost]
        //public ActionResult MemberLogin(Customer log, String FirstName,String LastName,String Email,String Password1,String Password2)
        //{

        //Customer customer = new Customer();



        /*if (Password1 != Password2)
            {
                return View();
            }
            else
            {
                customer.CustomerID = Email;
                customer.FirstName = FirstName;
                customer.LastName = LastName;
                customer.Email = Email;
                customer.Password = Password1;
                customer.RegisterDatetime = DateTime.Now;
                customer.Logging = DateTime.Now.ToString();
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("HomePage", "Home");

            }*/
        #endregion

        #region 方法二  欄位驗證
        [HttpPost]
        [MultiButton("register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(MixMemberLoginViewModel registerVM)
        {
            Customer customer = new Customer();

            if (ModelState.IsValid)
            {
                string firstname = HttpUtility.HtmlEncode(registerVM.MemberRegisterViewModel.FirstName);
                string lastname = HttpUtility.HtmlEncode(registerVM.MemberRegisterViewModel.LastName);
                string email = HttpUtility.HtmlEncode(registerVM.MemberRegisterViewModel.Email);
                string password = HttpUtility.HtmlEncode(registerVM.MemberRegisterViewModel.Password);

                var lnkHref = "<a href='" + Url.Action("MemberLogin", "MemberLogin", new { verify = email}, "https") + "'>Verify Your Account</a>";
                string subject = "Adoga Login - Verify Your Account";
                string body = "Hi" + "<br/><br/>這是你的確認信" +
                "<br/>" + lnkHref;


                WebMail.Send(email, subject, body, null, null, null, true, null, null, null, null, null, null);
                ViewBag.msg = "email ok";
                //1.View Model(memberregisterviewmodel) --> Data Model (customer)

                Customer cust = new Customer()
                {
                    CustomerID = email,
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    VerifyEmail = false,
                    MD5HashPassword = HashService.MD5Hash(password),
                    Logging = "建立" + "," + firstname + lastname + "," + DateTime.Now.ToString(),
                    RegisterDatetime = DateTime.Now

                };
                //EF
                try
                {
                    

                    _context.Customers.Add(cust);
                    _context.SaveChanges();
                    TempData["NewAccount"] = "帳號新增成功,請至信箱收取驗證信以完成驗證";
                    return RedirectToAction("MemberLogin", "MemberLogin");
                }
                catch (Exception)
                {
                    TempData["Account"] = "此帳號已經有人使用,請重新輸入新的帳號";
                    return RedirectToAction("MemberLogin", "MemberLogin");
                    
                }
            }

            return View(registerVM);
        }
        #endregion

        #region Login(登入)
        [HttpPost]
        [MultiButton("login")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(MixMemberLoginViewModel loginVM)
        {
            //一.未通過Model驗證
            if (!ModelState.IsValid)
            {

                return View(loginVM);
            }

            //二.通過Model驗證
            string email = HttpUtility.HtmlEncode(loginVM.MemberLoginViewModel.Email);
            string password = HashService.MD5Hash(HttpUtility.HtmlEncode(loginVM.MemberLoginViewModel.Password));

            //三.EF比對資料庫帳密
            //以Name及Password查詢比對Account資料表記錄

            Customer user = _context.Customers.Where(x => x.Email == email && x.MD5HashPassword == password).FirstOrDefault();

            

            //找不到則彈回Login頁
            if (user == null)
            {
                ModelState.AddModelError("NotFound", "無效的帳號或密碼!");

                return View(loginVM);
            }
            if (user.VerifyEmail == false || user.VerifyEmail==null)
            {
                ModelState.AddModelError("NotFound", "未通過認證,請至電子信箱確認郵件");

                return View(loginVM);
            }

            //四.FormsAuthentication Class -- https://docs.microsoft.com/zh-tw/dotnet/api/system.web.security.formsauthentication?view=netframework-4.8

            //FormsAuthenticationTicket Class
            //https://docs.microsoft.com/zh-tw/dotnet/api/system.web.security.formsauthenticationticket?view=netframework-4.8


            //1.Create FormsAuthenticationTicket
            var ticket = new FormsAuthenticationTicket(
            version: 1,
            name: user.FirstName + user.LastName.ToString(), //可以放使用者Id
            issueDate: DateTime.UtcNow,//現在UTC時間
            expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
            isPersistent: loginVM.MemberLoginViewModel.Remember,// 是否要記住我 true or false
            userData: JsonConvert.SerializeObject(new UserCookieViewModel { Id = email, PictureUrl = string.Empty }), //可以放使用者角色名稱 new UserCookieViewModel { Id = email,PictureUrl = null }
            cookiePath: FormsAuthentication.FormsCookiePath);

            //2.Encrypt the Ticket
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            //3.Create the cookie.
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(cookie);

            //4.Redirect back to original URL.
            var url = FormsAuthentication.GetRedirectUrl(email, true);

            //5.Response.Redirect
            return RedirectToAction("HomePage", "Home");
        }
        #endregion

        #region Facebook登入
        public ActionResult FacebookLogin(MixMemberLoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {

                return View(loginVM);
            }

            //二.通過Model驗證
            string email = HttpUtility.HtmlEncode(loginVM.MemberLoginViewModel.Email);
            string password = HashService.MD5Hash(HttpUtility.HtmlEncode(loginVM.MemberLoginViewModel.Password));

            //三.EF比對資料庫帳密
            //以Name及Password查詢比對Account資料表記錄

            Customer user = _context.Customers.Where(x => x.Email == email && x.MD5HashPassword == password).FirstOrDefault();

            //找不到則彈回Login頁
            if (user == null)
            {
                ModelState.AddModelError("NotFound", "無效的帳號或密碼!");

                return View(loginVM);
            }


            //四.FormsAuthentication Class -- https://docs.microsoft.com/zh-tw/dotnet/api/system.web.security.formsauthentication?view=netframework-4.8

            //FormsAuthenticationTicket Class
            //https://docs.microsoft.com/zh-tw/dotnet/api/system.web.security.formsauthenticationticket?view=netframework-4.8


            //1.Create FormsAuthenticationTicket
            var ticket = new FormsAuthenticationTicket(
            version: 1,
            name: user.FirstName + user.LastName.ToString(), //可以放使用者Id
            issueDate: DateTime.UtcNow,//現在UTC時間
            expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
            isPersistent: loginVM.MemberLoginViewModel.Remember,// 是否要記住我 true or false
            userData: loginVM.MemberLoginViewModel.Email, //可以放使用者角色名稱
            cookiePath: FormsAuthentication.FormsCookiePath);

            //2.Encrypt the Ticket
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            //3.Create the cookie.
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(cookie);

            //4.Redirect back to original URL.
            var url = FormsAuthentication.GetRedirectUrl(email, true);

            //5.Response.Redirect
            return RedirectToAction("HomePage", "Home");
        }
        #endregion

        #region facebook註冊
        [HttpPost]
        public async Task<ActionResult> FacebookLoginAPI(string Id, string Name, string Email, string Picture)
        {
            UserCookieViewModel UserData = new UserCookieViewModel()
            {
                Id = Id,
                PictureUrl = string.Empty
            };
            if (_context.Customers.Where(x => x.Email == Email).FirstOrDefault() == null)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Customer cust = new Customer()
                        {
                            CustomerID = Id,
                            FirstName = Name,
                            LastName = string.Empty,
                            Email = Email,
                            MD5HashPassword = string.Empty,
                            Logging = "建立" + "," + Name + "," + DateTime.Now.ToString(),
                            RegisterDatetime = DateTime.Now
                        };
                        _context.Customers.Add(cust);
                        _context.SaveChanges();

                        //1.Create FormsAuthenticationTicket
                        var ticket = new FormsAuthenticationTicket(
                        version: 1,
                        name: cust.FirstName + cust.LastName, //可以放使用者Id
                        issueDate: DateTime.UtcNow,//現在UTC時間
                        expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
                        isPersistent: false,// 是否要記住我 true or false
                        userData: JsonConvert.SerializeObject(UserData), //可以放使用者角色名稱
                        cookiePath: FormsAuthentication.FormsCookiePath);

                        //2.Encrypt the Ticket
                        var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                        //3.Create the cookie.
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        Response.Cookies.Add(cookie);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        //result.IsSuccessful = false;
                        //result.Exception = ex;
                        transaction.Rollback();
                    }
                }
            }
            else
            {
                //1.Create FormsAuthenticationTicket
                var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: Name, //可以放使用者Id
                issueDate: DateTime.UtcNow,//現在UTC時間
                expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
                isPersistent: false,// 是否要記住我 true or false
                userData: JsonConvert.SerializeObject(UserData), //可以放使用者角色名稱
                cookiePath: FormsAuthentication.FormsCookiePath);

                //2.Encrypt the Ticket
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                //3.Create the cookie.
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(cookie);
            }

            return Content("ok");

        }
        #endregion

        #region GoogleLogin(Google登入)
        [HttpPost]
        public async Task<ActionResult> GoogleLoginAPI(string id_token)
        {
            string msg = "ok";
            GoogleJsonWebSignature.Payload payload = null;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(id_token, new GoogleJsonWebSignature.ValidationSettings()
                {
                    //要驗證的client id，把自己申請的Client ID填進去
                    Audience = new List<string>() 
                    { "373077817054-5hahnkio91en8pnqqqpaginugjt0f85v.apps.googleusercontent.com" }
                });
            }
            catch (Google.Apis.Auth.InvalidJwtException ex)
            {
                msg = ex.Message;
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                msg = ex.Message;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg == "ok" && payload != null)
            {//都成功
                string GoogleMail = payload.Email;
                string GoogleFirstName = payload.GivenName;
                string GoogleLastName = payload.FamilyName;
                string GooglePicture = payload.Picture;
                UserCookieViewModel UserData = new UserCookieViewModel()
                {
                    Id = GoogleMail,
                    PictureUrl = GooglePicture
                };
                if (_context.Customers.Where(x => x.Email == GoogleMail).FirstOrDefault() == null)
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            Customer cust = new Customer()
                            {
                                CustomerID = GoogleMail,
                                FirstName = GoogleFirstName,
                                LastName = GoogleLastName,
                                Email = GoogleMail,
                                MD5HashPassword = string.Empty,
                                Logging = "建立" + "," + GoogleFirstName + GoogleLastName + "," + DateTime.Now.ToString(),
                                RegisterDatetime = DateTime.Now
                            };
                            _context.Customers.Add(cust);
                            _context.SaveChanges();

                            //1.Create FormsAuthenticationTicket
                            var ticket = new FormsAuthenticationTicket(
                            version: 1,
                            name: cust.FirstName + cust.LastName, //可以放使用者Id
                            issueDate: DateTime.UtcNow,//現在UTC時間
                            expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
                            isPersistent: false,// 是否要記住我 true or false
                            userData: JsonConvert.SerializeObject(UserData), //可以放使用者角色名稱
                            cookiePath: FormsAuthentication.FormsCookiePath);

                            //2.Encrypt the Ticket
                            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                            //3.Create the cookie.
                            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            Response.Cookies.Add(cookie);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            //result.IsSuccessful = false;
                            //result.Exception = ex;
                            transaction.Rollback();
                        }
                    }
                }

                else
                {
                    //1.Create FormsAuthenticationTicket
                    var ticket = new FormsAuthenticationTicket(
                    version: 1,
                    name: GoogleFirstName + GoogleLastName, //可以放使用者Id
                    issueDate: DateTime.UtcNow,//現在UTC時間
                    expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
                    isPersistent: false,// 是否要記住我 true or false
                    userData: JsonConvert.SerializeObject(UserData), //可以放使用者角色名稱
                    cookiePath: FormsAuthentication.FormsCookiePath);

                    //2.Encrypt the Ticket
                    var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                    //3.Create the cookie.
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(cookie);
                }



                //string user_id = payload.Subject;//取得user_id
                //msg = $@"您的 user_id :{user_id}";
            }

            return Content("OK");
        }
        #endregion

        #region LineLogin
        string redirect_uri = "https://adoga.azurewebsites.net/MemberLogin/callback";
        string client_id = "1656167198";
        string client_secret = "ef98822259547db7297ecb9606b357b1";

        public ActionResult LineLoginDirect()
        {
            string state = Guid.NewGuid().ToString();
            TempData["state"] = state;//利用TempData被取出資料後即消失的特性，來防禦CSRF攻擊
            string response_type = "code";
            string client_id = "1656167198";
            string redirect_uri = HttpUtility.UrlEncode("https://adoga.azurewebsites.net/MemberLogin/callback");
            string LineLoginUrl = string.Format("https://access.line.me/oauth2/v2.1/authorize?response_type={0}&client_id={1}&redirect_uri={2}&state={3}&scope=profile%20openid%20email",
                response_type,
                client_id,
                redirect_uri,
                state
                );
            return Redirect(LineLoginUrl);
        }

        public ActionResult callback(string state, string code, string error, string error_description)
        {
            if (!string.IsNullOrEmpty(error))
            {//用戶沒授權你的LineApp
                ViewBag.error = error;
                ViewBag.error_description = error_description;
                return View();
            }

            if (TempData["state"] == null)
            {//可能使用者停留Line登入頁面太久

                return Content("頁面逾期");

            }

            if (Convert.ToString(TempData["state"]) != state)
            {//使用者原先Request QueryString的TempData["state"]和Line導頁回來夾帶的state Querystring不一樣，可能是parameter tampering或CSRF攻擊

                return Content("state驗證失敗");

            }

            if (Convert.ToString(TempData["state"]) == state)
            {//state字串驗證通過

                //取得id_token和access_token:https://developers.line.biz/en/docs/line-login/web/integrate-line-login/#spy-getting-an-access-token
                string issue_token_url = "https://api.line.me/oauth2/v2.1/token";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(issue_token_url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                //必須透過ParseQueryString()來建立NameValueCollection物件，之後.ToString()才能轉換成queryString
                NameValueCollection postParams = HttpUtility.ParseQueryString(string.Empty);
                postParams.Add("grant_type", "authorization_code");
                postParams.Add("code", code);
                postParams.Add("redirect_uri", this.redirect_uri);
                postParams.Add("client_id", this.client_id);
                postParams.Add("client_secret", this.client_secret);

                //要發送的字串轉為byte[] 
                byte[] byteArray = Encoding.UTF8.GetBytes(postParams.ToString());
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(byteArray, 0, byteArray.Length);
                }//end using

                //API回傳的字串
                string responseStr = "";
                //發出Request
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        responseStr = sr.ReadToEnd();
                    }//end using  
                }


                LineLoginToken tokenObj = JsonConvert.DeserializeObject<LineLoginToken>(responseStr);
                string id_token = tokenObj.id_token;

                //方案總管>參考>右鍵>管理Nuget套件 搜尋 System.IdentityModel.Tokens.Jwt 來安裝
                var jst = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(id_token);
                LineUserProfile user = new LineUserProfile();
                //↓自行決定要從id_token的Payload中抓什麼user資料
                user.userId = jst.Payload.Sub;
                user.displayName = jst.Payload["name"].ToString();
                user.pictureUrl = jst.Payload["picture"].ToString();
                if (jst.Payload.ContainsKey("email") && !string.IsNullOrEmpty(Convert.ToString(jst.Payload["email"])))
                {//有包含email，使用者有授權email個資存取，並且用戶的email有值
                    user.email = jst.Payload["email"].ToString();
                }


                string access_token = tokenObj.access_token;
                ViewBag.access_token = access_token;
                #region 接下來是為了抓用戶的statusMessage狀態消息，如果你不想要可以省略不發出下面的Request

                //Social API v2.1 Getting user profiles
                //https://developers.line.biz/en/docs/social-api/getting-user-profiles/
                //取回User Profile
                string profile_url = "https://api.line.me/v2/profile";


                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(profile_url);
                req.Headers.Add("Authorization", "Bearer " + access_token);
                req.Method = "GET";
                //API回傳的字串
                string resStr = "";
                //發出Request
                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
                    {
                        resStr = sr.ReadToEnd();
                    }//end using  

                }

                LineUserProfile userProfile = JsonConvert.DeserializeObject<LineUserProfile>(resStr);
                user.statusMessage = userProfile.statusMessage;//補上狀態訊息

                #endregion

                ViewBag.user = JsonConvert.SerializeObject(user, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented
                });


                UserCookieViewModel UserData = new UserCookieViewModel()
                {
                    Id = user.email,
                    PictureUrl = string.Empty
                };
                if (_context.Customers.Where(x => x.Email == user.email).FirstOrDefault() == null)
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            Customer cust = new Customer()
                            {
                                CustomerID = user.email,
                                FirstName = user.displayName,
                                LastName = string.Empty,
                                Email = user.email,
                                MD5HashPassword = string.Empty,
                                Logging = "建立" + "," + user.displayName + "," + DateTime.Now.ToString(),
                                RegisterDatetime = DateTime.Now
                            };
                            _context.Customers.Add(cust);
                            _context.SaveChanges();

                            //1.Create FormsAuthenticationTicket
                            var ticket = new FormsAuthenticationTicket(
                            version: 1,
                            name: cust.FirstName + cust.LastName, //可以放使用者Id
                            issueDate: DateTime.UtcNow,//現在UTC時間
                            expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
                            isPersistent: false,// 是否要記住我 true or false
                            userData: JsonConvert.SerializeObject(UserData), //可以放使用者角色名稱
                            cookiePath: FormsAuthentication.FormsCookiePath);

                            //2.Encrypt the Ticket
                            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                            //3.Create the cookie.
                            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            Response.Cookies.Add(cookie);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            //result.IsSuccessful = false;
                            //result.Exception = ex;
                            transaction.Rollback();
                        }
                    }
                }
                else
                {
                    //1.Create FormsAuthenticationTicket
                    var ticket = new FormsAuthenticationTicket(
                    version: 1,
                    name: user.displayName, //可以放使用者Id
                    issueDate: DateTime.UtcNow,//現在UTC時間
                    expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
                    isPersistent: false,// 是否要記住我 true or false
                    userData: JsonConvert.SerializeObject(UserData), //可以放使用者角色名稱
                    cookiePath: FormsAuthentication.FormsCookiePath);

                    //2.Encrypt the Ticket
                    var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                    //3.Create the cookie.
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(cookie);
                }
            }//end if 

            return RedirectToAction("HomePage", "Home");
        }
        #endregion

        #region 寄送email
        public ActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        [MultiButton("useremail")]
        [ValidateAntiForgeryToken]
        public ActionResult SendEmail(MixMemberLoginViewModel ForgetVm)
        {

            var useremail = HttpUtility.HtmlEncode(ForgetVm.ForgetPasswordViewModel.Email);



            //var emailusername = _context.Customers.Where(x => x.Email == useremail).FirstOrDefault();
            //var aaa = from m in _context.Customers
            //          where m.Email == useremail
            //          select m;


            //select Name
            //from Customer
            //where Email = 'useremail'
            var lnkHref = "<a href='" + Url.Action("MemberLogin", "MemberLogin", new { email = useremail }, "https") + "'>Reset Password</a>";
            Content("ok");
            string subject = "Adoga Login - Password reset instructions";
            string body = "Hi" + "<br/><br/>Forget your password?no problem.Just click the link  to reset" +
            "<br/>" + lnkHref;


            WebMail.Send(useremail, subject, body, null, null, null, true, null, null, null, null, null, null);
            ViewBag.msg = "email ok";

            return View();
        }
        #endregion

        #region 重設密碼
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [MultiButton("ResetPassword")]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(MixMemberLoginViewModel ResetVM)
        {
            string email = Request.Params["email"];
            string FirstPassword = HttpUtility.HtmlEncode(ResetVM.ResetPasswordViewModel.Password);

            Customer cust = new Customer();
            cust.Email = email;
            cust.MD5HashPassword = HashService.MD5Hash(FirstPassword);
            AdogaContext db = new AdogaContext();
            var data = db.Customers.Find(email);
            data.MD5HashPassword = HashService.MD5Hash(FirstPassword);
            db.SaveChanges();
            return RedirectToAction("HomePage", "Home");

        }
        #endregion
        public ActionResult VerifyEmail()
        {
            return View();
        }
        [HttpPost]
        [MultiButton("Verify")]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyEmail(string VerifyEmailValue, string abc)
        {


            Customer cust = new Customer();
            cust.Email = VerifyEmailValue;
          
            AdogaContext db = new AdogaContext();

            var data = db.Customers.Find(VerifyEmailValue);

            data.VerifyEmail = true;

            db.SaveChanges();


            return RedirectToAction("HomePage", "Home");

        }
    }
}
