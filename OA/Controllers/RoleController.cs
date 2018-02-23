using System;
using System.Linq;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using Common;
using IBLL;
using Model;

namespace OA.Controllers
{
    public class RoleController : Controller
    {
        private IRoleInfoService RoleInfoService { set; get; }
        private IActionInfoService ActionInfoService { set; get; }

        // GET
        public ActionResult Index()
        {
            return View();
        }
        
        
        public ActionResult getData()
        {
            int page = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);

            int totalCount;
            var roleInfoList = RoleInfoService.LoadPageEntities<int>(page, pageSize, out totalCount,
                r => r.DelFlag == 0, r => r.ID, true);
            
            var temp = from r in roleInfoList.AsEnumerable()
                select new
                {
                    ID = r.ID,
                    roleName = r.RoleName,
                    sort = r.Sort,
                    remark = r.Remark,
                    changeTime = r.ModifiedOn.ToString("yyyy-M-d")
                };

            string jsonStr = SerializeHelper.toJsonString(new {rows = temp, total = totalCount});

            return Content(jsonStr);
        }

        public ActionResult AddRole()
        {
            if (Request.RequestType == "GET")
            {
                return View();
            }
            
            string roleName = Request["roleName"] == null ? "" : Request["roleName"].ToString();
            int sort = Request["sort"] == null ? 0 : int.Parse(Request["sort"]);
            string remark = Request["remark"] == null ? "" : Request["remark"].ToString();

            if (string.IsNullOrEmpty(roleName))
            {
                return Content("error");
            }

            RoleInfo roleInfo = new RoleInfo()
            {
                RoleName = roleName,
                Sort = sort.ToString(),
                Remark = remark,
                DelFlag = 0,
                SubTime = DateTime.Now,
                ModifiedOn = DateTime.Now
            };

            RoleInfoService.AddEntity(roleInfo);

            return Content("ok");

        }

        public ActionResult EditRole(RoleInfo roleInfo)
        {
            bool b = RoleInfoService.EditRole(roleInfo);

            return Content(b.ToString());
        }


        public ActionResult DeleteRole(int id)
        {
            bool b = RoleInfoService.DeleteRoleById(id);
            return Content(b.ToString());
        }


        public ActionResult EditAction()
        {
            if (Request["rid"]==null)
            {
                return Content("rid error");
            }

            int rid = int.Parse(Request["rid"]);
            
            if (Request.RequestType.Equals("GET"))
            {
                RoleInfo roleInfo = RoleInfoService.LoadEntities(r=>r.ID==rid).FirstOrDefault();
                if (roleInfo == null)
                {
                    return Content("rid no exist");
                }
                ViewBag.RoleInfo = roleInfo;
                var actionInfoList = ActionInfoService.LoadEntities(a=>a.DelFlag==0).ToList();
                var roleActionIdList = (from a in roleInfo.ActionInfo select a.ID).ToList();

                ViewBag.ActionInfoList = actionInfoList;
                ViewBag.RoleActionIdList = roleActionIdList;
                
                return View();
            }

            if (Request["aid"] == null)
            {
                return Content("aid error");

            }
            bool isPass = Request["isPass"] == "true";

            int aid = int.Parse(Request["aid"]);

            bool b = RoleInfoService.EditRoleAction(rid,aid, isPass);

            /*string[] aidList = aids.Split(','); 
            bool b = RoleInfoService.AddAction(rid, aidList);*/
            
            return Content(b.ToString());
            
        }
    }
}