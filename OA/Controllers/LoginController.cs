using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Common;
using IBLL;
using Model;

namespace OA.Controllers
{
    public class LoginController : Controller
    {
        private IUserInfoService UserInfoService { get; set; }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserLogin()
        {

/*            string vCode = Session["vCode"] != null ? Session["vCode"].ToString() : "";

            if (string.IsNullOrEmpty(vCode))
            {
                return Content("no:验证码错误");
            }

            Session["vCode"] = null;
            string txtCode = Request["code"];
            if (txtCode == null)
            {
                return Content("no:验证码为空");
            }
            if (!txtCode.Equals(vCode,StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("no:验证码错误");
            }*/


            string userName = Request["username"];
            string userPwd = Request["password"];
            var userInfo = UserInfoService.LoadEntities(u => u.UName == userName && u.UPwd == userPwd).FirstOrDefault();

            if (userInfo!=null)
            {
                string sessionId = Guid.NewGuid().ToString();
                bool isSuccess = MemcachedHelper.Set(sessionId, SerializeHelper.toJsonString(new{ID=userInfo.ID,UName=userInfo.UName}),DateTime.Now.AddMinutes(200));
                string o = MemcachedHelper.Get(sessionId).ToString();
                if (!isSuccess)
                {
                    return Content("no:登录失败，session设置失败");
                }
                Response.Cookies["sessionId"].Value = sessionId;
                return Content("ok:登录成功");
            }
            else
            {
                return Content("no:登录失败");
            }
        }

        public ActionResult ShowValidateCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(4);
            Session["vCode"] = code;
            byte[] buffer = vCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }
    }
}