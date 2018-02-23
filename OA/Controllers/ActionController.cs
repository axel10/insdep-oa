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
    public class ActionController : Controller
    {
        
        
        private IActionInfoService ActionInfoService { set; get; }
        // GET: Action
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult getData()
        {
            int page = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);

            int totalCount;
            var actionInfoList = ActionInfoService.LoadPageEntities<int>(page, pageSize, out totalCount,
                a => a.DelFlag == 0, a => a.ID, true);
            
            var temp = from a in actionInfoList.AsEnumerable()
                select new
                {
                    ID = a.ID,
                    ActionInfoName = a.ActionInfoName,
                    HttpMethod=a.HttpMethod,
                    Url=a.Url,
                    Remark = a.Remark,
                    changeTime = a.ModifiedOn.ToString("yyyy-M-d")
                };

            string jsonStr = SerializeHelper.toJsonString(new {rows = temp, total = totalCount});

            return Content(jsonStr);
        }
        
        
        public ActionResult AddAction(ActionInfo actionInfo)
        {
            if (Request.RequestType=="GET")
            {
                return View();
            }
            else
            {
                if (string.IsNullOrEmpty(actionInfo.ActionInfoName)|string.IsNullOrEmpty(actionInfo.Url)|string.IsNullOrEmpty(actionInfo.HttpMethod))
                {
                    return Content("error");
                }

                ActionInfo dbuActionInfo = ActionInfoService.LoadEntities(u => u.Url == actionInfo.Url|u.ActionInfoName==actionInfo.ActionInfoName).FirstOrDefault();
                if (dbuActionInfo!=null)
                {
                    return Content("repeat");
                }

                actionInfo.SubTime = DateTime.Now;
                actionInfo.ModifiedOn = DateTime.Now;
                
                ActionInfoService.AddEntity(actionInfo);
                return Content("ok");
            }
        }
        
        

        public ActionResult EditAction(ActionInfo actionInfo)
        {
            bool b = ActionInfoService.EditAction(actionInfo);

            return Content(b.ToString());
        }
        
        public ActionResult DeleteAction(int id)
        {
            bool b = ActionInfoService.DeleteActionById(id);
            return Content(b.ToString());
        }
    }
}