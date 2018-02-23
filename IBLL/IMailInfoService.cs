using System.Collections.Generic;
using System.Linq;
using Model;

namespace IBLL
{
    public partial interface IMailInfoService
    {
        bool SaveMailUserInfo(string[] userIdList, int mailId);
        ICollection<MailInfo> GetAllReceiveMail(int userId);
        IQueryable<MailInfo> GetAllReceiveMail(int userInfoId, string query);
        bool DeleteMails(int[] idList);
        List<MailInfo> GetAllDraftMail(int userInfoId);
        List<MailInfo> GetAllDraftMail(int userInfoId, string query);
    }
}