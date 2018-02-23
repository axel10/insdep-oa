using System;
using System.Collections.Generic;
using System.Linq;
using IBLL;
using Model;

namespace BLL
{
    public partial class MailInfoService : BaseService<MailInfo>, IMailInfoService
    {
        public bool SaveMailUserInfo(string[] userIdList, int mailId)
        {
            foreach (string userId in userIdList)
            {
                MailUserInfo info = new MailUserInfo
                {
                    Id = Guid.NewGuid(),
                    UserInfoId = int.Parse(userId),
                    MailInfoId = mailId
                };
                CurrentDBSession.MailUserInfoDal.AddEntity(info);
                CurrentDBSession.SaveChanges();
            }

            return true;
        }

        public ICollection<MailInfo> GetAllReceiveMail(int userId)
        {
            UserInfo userInfo = CurrentDBSession.UserInfoDal.LoadEntites(u => u.ID == userId).FirstOrDefault();
            /*return (from mail in userInfo.MailInfo
                orderby mail.CreateTime descending
                select mail).ToList();*/
            return userInfo.MailInfo.OrderByDescending(m=>m.CreateTime).ToList();
        }

        public IQueryable<MailInfo> GetAllReceiveMail(int userInfoId, string query)
        {
            var mailInfos = CurrentDal.LoadEntites(m => m.Title.Contains(query));
            return mailInfos;
        }

        public bool DeleteMails(int[] idList)
        {
            foreach (int id in idList)
            {
                MailInfo mailInfo = CurrentDal.LoadEntites(m => m.Id == id).FirstOrDefault();
                bool b = CurrentDal.DeleteEntity(mailInfo);
            }
            CurrentDBSession.SaveChanges();
            return true;
        }

        
        public List<MailInfo> GetAllDraftMail(int userId)
        {
//            UserInfo userInfo = CurrentDBSession.UserInfoDal.LoadEntites(u => u.ID == userId).FirstOrDefault();

            return CurrentDal.LoadEntites(m => m.SenderId == userId && m.IsDraft == true).ToList();

//            return userInfo.MailInfo.Where(m=>m.IsDraft==true).OrderByDescending(m=>m.CreateTime).ToList();
        }

        public List<MailInfo> GetAllDraftMail(int userInfoId, string query)
        {
/*            UserInfo userInfo = CurrentDBSession.UserInfoDal.LoadEntites(u => u.ID == userInfoId).FirstOrDefault();
            return userInfo.MailInfo.Where(m=>m.IsDraft==true).OrderByDescending(m=>m.CreateTime).ToList();
            */
            return CurrentDal.LoadEntites(m => m.SenderId == userInfoId && m.IsDraft == true&&m.Title.Contains(query)).ToList();
        }
    }
}