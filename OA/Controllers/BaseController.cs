using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Model;

namespace OA.Controllers
{
    public class BaseController : Controller
    {
        public UserInfo LoginUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            bool isSuccess = false;

            if (Request.Cookies["sessionId"]!=null)
            {
                string sessionId = Request.Cookies["sessionId"].Value;
                object obj = MemcachedHelper.Get(sessionId);
                if (obj!=null)
                {
                    UserInfo userInfo = SerializeHelper.toJson<UserInfo>(obj.ToString());
                    LoginUser = userInfo;
                    isSuccess = true;
                    MemcachedHelper.Set(sessionId, obj.ToString(), DateTime.Now.AddMinutes(60));

                    if (LoginUser.UName=="123")
                    {
                        return;
                    }

                }
            }

            if (!isSuccess)
            {
                filterContext.Result = Redirect("/login/index");
            }
        }
    }
}