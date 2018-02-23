using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using IBLL;
using Model;

namespace OA.Controllers
{
    public class UserController : Controller
    {
        private IUserInfoService UserInfoService { set; get; }
        private IActionInfoService ActionInfoService { set; get; }
        private IRoleInfoService RoleInfoService { set; get; }

        // GET: UserInfo
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult getData()
        {
            int page = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);

            int totalCount;
            var userInfoList = UserInfoService.LoadPageEntities<int>(page, pageSize, out totalCount,
                r => r.DelFlag == 0, r => r.ID, true);
            
            var temp = from u in userInfoList.AsEnumerable()
                select new
                {
                    ID = u.ID,
                    UName = u.UName,
                    remark = u.Remark,
                    changeTime = u.ModifiedOn.ToString("yyyy-M-d")
                };

            string jsonStr = SerializeHelper.toJsonString(new {rows = temp, total = totalCount});

            return Content(jsonStr);
        }

        public ActionResult AddUser(UserInfo userInfo)
        {
            if (Request.RequestType=="GET")
            {
                return View();
            }
            else
            {
                if (string.IsNullOrEmpty(userInfo.UName)|string.IsNullOrEmpty(userInfo.UPwd))
                {
                    return Content("error");
                }

                UserInfo dbuUserInfo = UserInfoService.LoadEntities(u => u.UName == userInfo.UName).FirstOrDefault();

                userInfo.SubTime = DateTime.Now;
                userInfo.ModifiedOn = DateTime.Now;
                
                UserInfoService.AddEntity(userInfo);
                return Content("ok");
            }
        }
        
        
        public ActionResult EditUser(UserInfo userInfo)
        {
            bool b = UserInfoService.EditUser(userInfo);

            return Content(b.ToString());
        }
        
        
        public ActionResult DeleteUser(int id)
        {
            bool b = UserInfoService.DeleteUserById(id);
            return Content(b.ToString());
        }

        public ActionResult ShowUserAction(int id)
        {
            if (id == null)
            {
                return Content("error");
            }

            UserInfo userInfo = UserInfoService.LoadEntities(u => u.ID == id).FirstOrDefault();
            ViewBag.UserInfo = userInfo;
            var allActionInfos = ActionInfoService.LoadEntities(a => a.DelFlag == 0).ToList();
            var allActionId = (from aState in userInfo.R_UserInfo_ActionInfo select aState).ToList();

            ViewBag.AllActionInfos = allActionInfos;
            ViewBag.allActionId = allActionId;
            return View();
        }


        public ActionResult SetUserAction(int aid, int uid)
        {
            bool isPass = Request["isPass"].Equals("true");

            if (UserInfoService.SetUserActionInfo(aid,uid,isPass))
            {
                return Content("ok");
            }
            
            return Content("error");
        }
        
        
        public ActionResult EditUserRole()
        {
            if (Request["uid"]==null)
            {
                return Content("uid error");
            }

            int uid = int.Parse(Request["uid"]);
            
            if (Request.RequestType.Equals("GET"))
            {
                UserInfo userInfo = UserInfoService.LoadEntities(u=>u.ID==uid).FirstOrDefault();
                if (userInfo == null)
                {
                    return Content("uid no exist");
                }
                ViewBag.UserInfo = userInfo;
                var roleInfoList = RoleInfoService.LoadEntities(r=>r.DelFlag==0).ToList();
                var roleIdList = (from r in userInfo.RoleInfo select r.ID).ToList();

                ViewBag.RoleInfoList = roleInfoList;
                ViewBag.RoleIdList = roleIdList;
                
                return View();
            }

            if (Request["rid"] == null)
            {
                return Content("rid error");

            }
            bool isPass = Request["isPass"] == "true";

            int rid = int.Parse(Request["rid"]);

            bool b = UserInfoService.EditUserRole(rid,uid, isPass);

            /*string[] ridList = rids.Split(','); 
            bool b = RoleInfoService.AddAction(rid, ridList);*/
            
            return Content(b.ToString());
            
        }
    }
}