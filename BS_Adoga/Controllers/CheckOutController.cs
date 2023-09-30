using BS_Adoga.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.ViewModels.CheckOut;
using ECPay.Payment.Integration;
using System.Collections;
using System.Web.Security;
using System.Data.Entity;
using BS_Adoga.Models.ViewModels.MemberLogin;
using Newtonsoft.Json;
using BS_Adoga.Models.ViewModels.Account;

namespace BS_Adoga.Controllers
{
    
    public class CheckOutController : Controller
    {
        private AdogaContext _context;
        public CheckOutController()
        {
            _context = new AdogaContext();
        }

        [Authorize]
        // GET: CheckOut
        public ActionResult Index()
        {
            OrderVM orderData = (OrderVM)TempData.Peek("orderData");

            //OrderVM orderData = (OrderVM)TempData["orderData"];

            //List<SelectListItem> selectCountry = new List<SelectListItem>() {
            //    new SelectListItem { Text = "台灣", Value = "台灣" },
            //    new SelectListItem { Text = "日本", Value = "日本" },
            //    new SelectListItem { Text = "韓國", Value = "韓國" },
            //    new SelectListItem { Text = "美國", Value = "美國" },
            //    new SelectListItem { Text = "澳洲", Value = "澳洲" }
            //};
            //ViewBag.SelectCountry = selectCountry;

            //ViewData["MySkills"] = mySkills;

            //List<SelectListItem> items = new List<SelectListItem>();
            //items.Add(new SelectListItem { Text = "Kirin", Value = "29" });
            //items.Add(new SelectListItem { Text = "Jade", Value = "28", Selected = true });
            //items.Add(new SelectListItem { Text = "Yao", Value = "24" });
            //ViewBag.list = items;

            return View(orderData);
        }

        /// <summary>
        /// 送出表單資料，新增至資料庫
        /// </summary>
        /// <param name="orderVM"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(OrderVM orderVM)
        {
            OrderVM orderData = (OrderVM)TempData.Peek("orderData");


            if (ModelState.IsValid)
            {
                //1.View Model(RegisterViewModel) --> Data Model (HotelEmployee)
                string firstname = HttpUtility.HtmlEncode(orderVM.checkOutListViewModel.FirstName);
                string lastname = HttpUtility.HtmlEncode(orderVM.checkOutListViewModel.LastName);
                string email = HttpUtility.HtmlEncode(orderVM.checkOutListViewModel.Email);
                string ConfirmEmail = HttpUtility.HtmlEncode(orderVM.checkOutListViewModel.ConfirmEmail);
                string phonenumber = HttpUtility.HtmlEncode(orderVM.checkOutListViewModel.PhoneNumber);
                //string customerId = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;

                string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
                UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
                string customer = UserCookie.Id;

                Order od = new Order()
                {
                    OrderID = "Adoga" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    CustomerID = customer,
                    RoomID = orderData.roomCheckOutViewModel.RoomID,
                    OrderDate = DateTime.Now,
                    CheckInDate = DateTime.Parse(orderData.roomCheckOutViewModel.CheckInDate + " 15:00:00"),
                    CheckOutDate = DateTime.Parse(orderData.roomCheckOutViewModel.CheckOutDate + " 11:00:00"),
                    RoomCount = orderData.roomCheckOutViewModel.RoomOrder,
                    RoomPriceTotal = Math.Ceiling(orderData.roomCheckOutViewModel.TotalPrice),
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    PhoneNumber = phonenumber,
                    Country = orderVM.checkOutListViewModel.countries,
                    SmokingPreference = orderVM.checkOutListViewModel.SmokingPreference,
                    BedPreference = orderVM.checkOutListViewModel.BedPreference,
                    ArrivingTime = orderVM.checkOutListViewModel.ArrivingTime,
                    PaymentStatus = false,
                    Logging = "建立" + "," + firstname + lastname + "," + DateTime.Now.ToString(),
                };


                using (var tran = _context.Database.BeginTransaction())
                {

                    //EF
                    try
                    {
                        _context.Orders.Add(od);
                        _context.SaveChanges();
                        TempData["OrderId"] = od.OrderID;
                        tran.Commit();
                        return RedirectToAction("PayAPI");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return Content("訂單建立失敗:" + ex.ToString());
                    }
                }
            }

            return View(orderVM);
        }


        public ActionResult PayAPI(PayApiInfo info)
        {
            string orderId = (string)TempData["OrderId"];           
            //From Index Form
            OrderVM orderData = (OrderVM)TempData.Peek("orderData");
            //From Account
            RePayViewModel ReOrderData = (RePayViewModel)TempData["ReOrderData"];

            if (ReOrderData == null)
            {
                info.OrderId = orderId;
                info.TotalPrice = Math.Ceiling(orderData.roomCheckOutViewModel.TotalPrice);
                info.HotelName = orderData.roomCheckOutViewModel.HotelFullName;
                info.RoomQuantity = orderData.roomCheckOutViewModel.RoomOrder;                
            }
            else
            {
                info.OrderId = ReOrderData.OrderID + new Random().Next(0, 10).ToString();
                info.TotalPrice = Math.Ceiling(ReOrderData.RoomPriceTotal);
                info.HotelName = ReOrderData.HotelName;
                info.RoomQuantity = ReOrderData.RoomQuantity;
            }


            List<string> enErrors = new List<string>();
            string payment = String.Empty;
            try
            {
                using (AllInOne oPayment = new AllInOne())
                {
                    /* 服務參數 */
                    oPayment.ServiceMethod = HttpMethod.HttpPOST;//介接服務時，呼叫 API 的方法
                    oPayment.ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";//要呼叫介接服務的網址
                    oPayment.HashKey = "5294y06JbISpM5x9";//ECPay提供的Hash Key
                    oPayment.HashIV = "v77hoKGq4kWxNNIS";//ECPay提供的Hash IV
                    oPayment.MerchantID = "2000214";//ECPay提供的特店編號 2000132

                    /* 基本參數 */
                    oPayment.Send.ReturnURL = "https://adoga.azurewebsites.net/CheckOut/PayFeedback";//付款完成通知回傳的網址https://localhost:44352/CheckOut/PayFeedback
                    oPayment.Send.ClientBackURL = "http://adoga.azurewebsites.net/";//瀏覽器端返回的廠商網址
                    oPayment.Send.OrderResultURL = "https://adoga.azurewebsites.net/CheckOut/PayFeedback";//瀏覽器端回傳付款結果網址
                    oPayment.Send.MerchantTradeNo = info.OrderId;//廠商的交易編號
                    oPayment.Send.MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");//廠商的交易時間
                    oPayment.Send.TotalAmount = info.TotalPrice;//交易總金額
                    oPayment.Send.TradeDesc = "交易描述";//交易描述
                    oPayment.Send.ChoosePayment = PaymentMethod.Credit;//使用的付款方式
                    oPayment.Send.Remark = "Test";//備註欄位
                    oPayment.Send.ChooseSubPayment = PaymentMethodItem.None;//使用的付款子項目
                    oPayment.Send.NeedExtraPaidInfo = ExtraPaymentInfo.No;//是否需要額外的付款資訊
                    oPayment.Send.DeviceSource = DeviceType.PC;//來源裝置
                    oPayment.Send.IgnorePayment = ""; //不顯示的付款方式
                    oPayment.Send.PlatformID = "";//特約合作平台商代號
                    oPayment.Send.HoldTradeAMT = HoldTradeType.Yes;
                    oPayment.Send.CustomField1 = "";
                    oPayment.Send.CustomField2 = "";
                    oPayment.Send.CustomField3 = "";
                    oPayment.Send.CustomField4 = "";
                    oPayment.Send.EncryptType = 1;

                    //訂單的商品資料
                    oPayment.Send.Items.Add(new Item()
                    {
                        Name = info.HotelName,//商品名稱
                        Price = info.TotalPrice,//商品單價
                        Currency = "新台幣",//幣別單位
                        Quantity = info.RoomQuantity,//購買數量
                        URL = "http://google.com",//商品的說明網址

                    });

                    /***************************信用卡額外功能參數***************************/

                    #region Credit 功能參數

                    //oPayment.SendExtend.BindingCard = BindingCardType.No; //記憶卡號
                    //oPayment.SendExtend.MerchantMemberID = ""; //記憶卡號識別碼
                    //oPayment.SendExtend.Language = "ENG"; //語系設定

                    #endregion Credit 功能參數

                    #region 一次付清

                    //oPayment.SendExtend.Redeem = false;   //是否使用紅利折抵
                    //oPayment.SendExtend.UnionPay = true; //是否為銀聯卡交易

                    #endregion

                    #region 分期付款

                    //oPayment.SendExtend.CreditInstallment = 3;//刷卡分期期數

                    #endregion 分期付款

                    #region 定期定額

                    //oPayment.SendExtend.PeriodAmount = 1000;//每次授權金額
                    //oPayment.SendExtend.PeriodType = PeriodType.Day;//週期種類
                    //oPayment.SendExtend.Frequency = 1;//執行頻率
                    //oPayment.SendExtend.ExecTimes = 2;//執行次數
                    //oPayment.SendExtend.PeriodReturnURL = "";//伺服器端回傳定期定額的執行結果網址。

                    #endregion


                    /* 產生訂單 */
                    //enErrors.AddRange(oPayment.CheckOut());
                    enErrors.AddRange(oPayment.CheckOutString(ref payment));
                }
            }
            catch (Exception ex)
            {
                // 例外錯誤處理。
                enErrors.Add(ex.Message);
            }
            finally
            {
                // 顯示錯誤訊息。
                if (enErrors.Count() > 0)
                {
                    // string szErrorMessage = String.Join("\\r\\n", enErrors);
                }
            }

            ViewBag.CreditPay = payment;
            return View();
        }
      
        [HttpPost]
        public ActionResult PayFeedback(ECPayResultsViewModel PayResult)
        {

            List<string> enErrors = new List<string>();
            Hashtable htFeedback = null;
            try
            {
                using (AllInOne oPayment = new AllInOne())
                {
                    oPayment.HashKey = "5294y06JbISpM5x9";//ECPay 提供的 HashKey
                    oPayment.HashIV = "v77hoKGq4kWxNNIS";//ECPay 提供的 HashIV
                    /* 取回付款結果 */
                    enErrors.AddRange(oPayment.CheckOutFeedback(ref htFeedback));
                }
                // 取回所有資料
                if (enErrors.Count() == 0)
                {
                    /* 支付後的回傳的基本參數 */
                    string szMerchantID = String.Empty;
                    string szMerchantTradeNo = String.Empty;
                    string szPaymentDate = String.Empty;
                    string szPaymentType = String.Empty;
                    string szPaymentTypeChargeFee = String.Empty;
                    string szRtnCode = String.Empty;
                    string szRtnMsg = String.Empty;
                    string szSimulatePaid = String.Empty;
                    string szTradeAmt = String.Empty;
                    string szTradeDate = String.Empty;
                    string szTradeNo = String.Empty;
                    // 取得資料
                    foreach (string szKey in htFeedback.Keys)
                    {
                        switch (szKey)
                        {
                            /* 支付後的回傳的基本參數 */
                            case "MerchantID": szMerchantID = htFeedback[szKey].ToString(); break;
                            case "MerchantTradeNo": szMerchantTradeNo = htFeedback[szKey].ToString(); break;
                            case "PaymentDate": szPaymentDate = htFeedback[szKey].ToString(); break;
                            case "PaymentType": szPaymentType = htFeedback[szKey].ToString(); break;
                            case "PaymentTypeChargeFee": szPaymentTypeChargeFee = htFeedback[szKey].ToString(); break;
                            case "RtnCode": szRtnCode = htFeedback[szKey].ToString(); break;
                            case "RtnMsg": szRtnMsg = htFeedback[szKey].ToString(); break;
                            case "SimulatePaid": szSimulatePaid = htFeedback[szKey].ToString(); break;
                            case "TradeAmt": szTradeAmt = htFeedback[szKey].ToString(); break;
                            case "TradeDate": szTradeDate = htFeedback[szKey].ToString(); break;
                            case "TradeNo": szTradeNo = htFeedback[szKey].ToString(); break;
                            default: break;
                        }
                    }
                }
                else
                {
                    // 其他資料處理。
                }
            }
            catch (Exception ex)
            {
                // 例外錯誤處理。
                enErrors.Add(ex.Message);
            }
            finally
            {

                // 回覆成功訊息。
                if (enErrors.Count() == 0)
                {
                    
                    //ECPayResultsViewModel PayResult = new ECPayResultsViewModel();
                    PayResult.OrderId = (string)htFeedback["MerchantTradeNo"];
                    PayResult.TradeDate = (string)htFeedback["TradeDate"];
                    PayResult.PaymentDate = (string)htFeedback["PaymentDate"];
                    PayResult.PaymentType = (string)htFeedback["PaymentType"];
                    PayResult.TradePrice = (string)htFeedback["TradeAmt"];
                    TempData["PayResult"] = PayResult;

                    //更新付款資訊
                    if (PayResult.OrderId.Length == 20)
                    {
                        string ReOrderId = PayResult.OrderId.Substring(0, PayResult.OrderId.Length - 1);
                        var payStatus = _context.Orders.Where(x => x.OrderID == ReOrderId).First();
                        payStatus.PaymentStatus = true;
                        //payStatus.Logging = payStatus.Logging + "; " + "已付款: " + ReOrderId + "(" + PayResult.OrderId.Substring(PayResult.OrderId.Length - 1) + ")";
                        payStatus.Logging = payStatus.Logging + "; " + "已付款: " + PayResult.OrderId;
                        _context.Entry(payStatus).State = EntityState.Modified;
                    }
                    else 
                    {
                        var payStatus = _context.Orders.Where(x => x.OrderID == PayResult.OrderId).First();
                        payStatus.PaymentStatus = true;
                        //payStatus.Logging = payStatus.Logging + "; " + "已付款: " + PayResult.OrderId;
                        _context.Entry(payStatus).State = EntityState.Modified;
                    }
                                      
                    _context.SaveChanges();
                    this.Response.Clear();

                    //List<string> list = htFeedback.Keys.Cast<string>().ToList();
                    //List<string> list2 = htFeedback.Values.Cast<string>().ToList();

                    //foreach (string s in htFeedback.Keys)
                    //{
                    //    this.Response.Write(s + " = " + htFeedback[s] + "</br>");
                    //}
                    //this.Response.Write("OK");
                    //this.Session["PayResult"] = PayResult;
                    //this.Response.Redirect("https://localhost:44352/CheckOut/ECPayResult");
                    //this.Server.Transfer("https://localhost:44352/CheckOut/ECPayResult");

                }
                // 回覆錯誤訊息。
                else
                    this.Response.Write(String.Format("0|{0}", String.Join("\\r\\n", enErrors)));
                //this.Response.Flush();
                //this.Response.End();
            }
            return RedirectToAction("ECPayResult", "CheckOut");
        }

        public ActionResult ECPayResult(ECPayResultsViewModel model)
        {
            //var hotelName = _context.Orders.Where(x => x.OrderID == model.OrderId).First();
            model = (ECPayResultsViewModel)TempData.Peek("PayResult");

            #region 當付款成功時roomdetail要增加order幾間
            var OrderIdLength = model.OrderId.Length;
            Order order = null;
            if(OrderIdLength == 19)
            { 
               order = _context.Orders.Where(x => x.OrderID == model.OrderId).FirstOrDefault();
            }
            else
            {
                string orderid = model.OrderId.Substring(0, 19);
                order = _context.Orders.Where(x => x.OrderID == orderid).FirstOrDefault();
            }

            if (order != null)
            {
                //住幾天
                var night = new TimeSpan(order.CheckOutDate.Ticks - order.CheckInDate.Ticks).Days + 1;
                for (int i = 0; i < night; i++)
                {
                    var CheckInDate = order.CheckInDate.AddDays(i); //如果住一晚則是加0(當天日期) 以此類推
                    var roomdetail = _context.RoomsDetails.Where(x => x.RoomID == order.RoomID && x.CheckInDate == CheckInDate).FirstOrDefault();
                    roomdetail.RoomOrder += order.RoomCount;
                    using (var tran = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            _context.Entry(roomdetail).State = EntityState.Modified;
                            _context.SaveChanges();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            return Content("付款成功但是房間數roomdetail增加失敗:" + ex.ToString());
                        }
                    }
                }
            }
            
            #endregion

            return View(model);
        }
    }
}