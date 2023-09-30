using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace BS_Adoga.Service
{
    public class HotelLoginAuthorizeAttribute : AuthorizeAttribute
    {
        private string _notifyUrl = "~/HotelLogin/HotelLogin";
        public string NotifyUrl
        {
            get { return _notifyUrl; }
            set { _notifyUrl = value; }
        }

        // AuthorizeAttribute包含三個關鍵方法供您override

        // 核心Function
        // 可以自定義module邏輯，若沒有要自己處理整個module架構，基本上不覆寫
        // Source Code: (https://github.com/mono/aspnetwebstack/blob/master/src/System.Web.Mvc/AuthorizeAttribute.cs) 
        public override void OnAuthorization(AuthorizationContext filterContext) 
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (AuthorizeCore(filterContext.HttpContext))
            {
                HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                cachePolicy.AddValidationCallback(CacheValidateHandler, null);
                //if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                //{
                //    //以下的 UsersHelper.UsersInfo().UserRoles是自行建立的
                //    if (!Roles.Split(',').Where(x => x == UsersHelper.UsersInfo().UserRoles.ToString()).Any())
                //    {
                //        //導向登入頁面(錯誤)
                //        HandleUnauthorizedRequest(filterContext);
                //    }
                //}
                //filterContext.Result = new RedirectResult("~/Function/Index");
            }
            else if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                HandleUnauthorizedRequest(filterContext);
            }
            //尚未驗證導向登入頁
            else
            {
                if (NotifyUrl != null)
                    filterContext.Result = new RedirectResult(NotifyUrl);
                else
                    //導向登入頁面
                    HandleUnauthorizedRequest(filterContext);
                //導向登入頁面(錯誤)
                //HandleUnauthorizedRequest(filterContext);
            }
        }

        // 驗證邏輯，成功回傳True，失敗回傳False
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            IPrincipal user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }
            return true;
        }

        // 驗證失敗要做什麼
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Returns HTTP 401 - see comment in HttpUnauthorizedResult.cs.
            filterContext.Result = new HttpUnauthorizedResult();
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }
    }
}