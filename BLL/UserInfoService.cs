using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IBLL;
using IDAL;
using Model;
using Model.Search;

namespace BLL
{
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        public bool DeleteEntity(UserInfo entity)
        {
            throw new NotImplementedException();
        }

/*        public async bool Demo()
        {
            await CurrentDBSession.ActionInfoDal.LoadEntites()
        }*/

        public bool DeleteEntities(List<int> list)
        {
            throw new NotImplementedException();
        }

        public bool SetUserRoleInfo(int userId, List<int> roleIdList)
        {
            throw new NotImplementedException();
        }

        public bool SetUserActionInfo(int actionId, int userId, bool isPass)
        {
            R_UserInfo_ActionInfo rUserInfoActionInfo = CurrentDBSession.R_UserInfo_ActionInfoDal
                .LoadEntites(a => a.ActionInfoID == actionId && a.UserInfoID == userId).FirstOrDefault();
            if (rUserInfoActionInfo == null)
            {
                var newRUserInfoActionInfo = new R_UserInfo_ActionInfo()
                {
                    ActionInfoID = actionId,
                    UserInfoID = userId,
                    IsPass = isPass
                };
                CurrentDBSession.R_UserInfo_ActionInfoDal.AddEntity(newRUserInfoActionInfo);
                CurrentDBSession.SaveChanges();
            }
            else
            {
                rUserInfoActionInfo.IsPass = isPass;
                CurrentDBSession.R_UserInfo_ActionInfoDal.EditEntity(rUserInfoActionInfo);
                CurrentDBSession.SaveChanges();
            }

            return true;
        }

        public bool EditUser(UserInfo userInfo)
        {
            UserInfo dbUser = CurrentDal.LoadEntites(u => u.ID == userInfo.ID).FirstOrDefault();
            dbUser.UName = userInfo.UName;
            dbUser.Remark = userInfo.Remark == null ? "" : userInfo.Remark;
            dbUser.ModifiedOn = DateTime.Now;
            return CurrentDBSession.SaveChanges();
        }

        public bool DeleteUserById(int id)
        {
            var userInfos = CurrentDBSession.UserInfoDal.LoadEntites(r => r.ID == id);
            UserInfo userInfo = userInfos.FirstOrDefault();
            CurrentDBSession.UserInfoDal.DeleteEntity(userInfo);
            return CurrentDBSession.SaveChanges();
        }

        public bool EditUserRole(int rid, int uid, bool isPass)
        {
            UserInfo userInfo = CurrentDal.LoadEntites(u => u.ID == uid).FirstOrDefault();
            if (userInfo == null)
            {
                return false;
            }

            RoleInfo dbRoleInfo = CurrentDBSession.RoleInfoDal.LoadEntites(r => r.ID == rid).FirstOrDefault();

            if (isPass)
            {
                userInfo.RoleInfo.Add(dbRoleInfo);
            }
            else
            {
                userInfo.RoleInfo.Remove(dbRoleInfo);
            }

            return CurrentDBSession.SaveChanges();
        }
    }
}