using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using IBLL;
using Model;

namespace OA.Controllers
{
    public class MailController : BaseController
    {
        private IMailInfoService MailInfoService { set; get; }
        private IUserInfoService UserInfoService { set; get; }


        public ActionResult Inbox(string query)
        {
            string sessionId = Request.Cookies["sessionId"]?.Value ?? "";
            string jsonStr = MemcachedHelper.Get(sessionId).ToString();
            UserInfo userInfo = SerializeHelper.toJson<UserInfo>(jsonStr);
            if (string.IsNullOrEmpty(query))
            {
                var mailInfos = MailInfoService.GetAllReceiveMail(userInfo.ID).ToList();
                ViewBag.MailInfoList = mailInfos;
                return View();
            }
            else
            {
                var mailInfos = MailInfoService.GetAllReceiveMail(userInfo.ID,query).ToList();
                ViewBag.MailInfoList = mailInfos.ToList();
                return View();
            }
        }

        public ActionResult DraftBox(string query)
        {
            string sessionId = Request.Cookies["sessionId"]?.Value ?? "";
            UserInfo userInfo = WebCommon.GetCurrentUser(sessionId);
            
            if (string.IsNullOrEmpty(query))
            {
                var mailInfos = MailInfoService.GetAllDraftMail(userInfo.ID);
                ViewBag.MailInfoList = mailInfos;
                return View();
            }
            else
            {
                var mailInfos = MailInfoService.GetAllDraftMail(userInfo.ID,query);
                ViewBag.MailInfoList = mailInfos;
                return View();
            }
        }

        public ActionResult WriteMail()
        {
            return View();
        }


        public ActionResult DeleteMails(int[] idList)
        {
            bool b = MailInfoService.DeleteMails(idList);
            return Content(b.ToString());
        }

        public ActionResult GetAllUserTag()
        {
            var userInfos = UserInfoService.LoadEntities(u=>u.DelFlag==0);
            var temp = from userInfo in userInfos.AsEnumerable()
                select new
                {
                    ID = userInfo.ID,
                    userName = userInfo.UName
                };


            return Content(SerializeHelper.toJsonString(temp));
        }

        // GET: Mail
        [ValidateInput(false)]
        public ActionResult SaveDraft(string title, string[] shoujian, string content, HttpPostedFileBase file)
        {
            string sessionId = Request.Cookies["sessionId"]?.Value ?? "";
            if (string.IsNullOrEmpty(sessionId))
            {
                return Redirect("/login/index");
            }
            SaveMail(title,shoujian,content,file,true,sessionId);
            return Content("ok");
        }
        
        [ValidateInput(false)]
        public ActionResult SendMail(string title, string[] shoujian, string content, HttpPostedFileBase file)
        {
            string sessionId = Request.Cookies["sessionId"]?.Value ?? "";
            if (string.IsNullOrEmpty(sessionId))
            {
                return Redirect("/login/index");
            }
            SaveMail(title,shoujian,content,file,false,sessionId);
            return Content("ok");
        }

        private void SaveMail(string title, string[] shoujian, string content, HttpPostedFileBase file, bool isDraft,string sessionId)
        {

            string jsonStr = MemcachedHelper.Get(sessionId).ToString();
            UserInfo userInfo = SerializeHelper.toJson<UserInfo>(jsonStr);

            MailInfo mailInfo;

            if (file!=null)
            {
                mailInfo = new MailInfo
                {
                    Title = title,
                    Content = content,
                    SenderId = userInfo.ID,
                    CreateTime = DateTime.Now,
                    File = file.FileName,
                    IsDraft = isDraft
                };
            }
            else
            {
                mailInfo = new MailInfo
                {
                    Title = title,
                    Content = content,
                    SenderId = userInfo.ID,
                    CreateTime = DateTime.Now,
                    IsDraft = isDraft
                };
            }
            MailInfoService.AddEntity(mailInfo);
            MailInfoService.SaveMailUserInfo(shoujian, mailInfo.Id);
        }
    }
}