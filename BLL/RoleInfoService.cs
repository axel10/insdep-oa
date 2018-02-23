using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLL;
using Model;

namespace BLL
{
    public partial class RoleInfoService : BaseService<RoleInfo>, IRoleInfoService
    {
        public bool EditRole(RoleInfo role)
        {
            RoleInfo dbRole = CurrentDal.LoadEntites(r => r.ID == role.ID).FirstOrDefault();
            dbRole.RoleName = role.RoleName;
            dbRole.Remark = role.Remark == null?"":role.Remark;
            dbRole.Sort = role.Sort;
            dbRole.ModifiedOn = DateTime.Now;
            return CurrentDBSession.SaveChanges();
        }

        public bool DeleteRoleById(int id)
        {
            var roleInfos = CurrentDBSession.RoleInfoDal.LoadEntites(r => r.ID == id);
            RoleInfo roleInfo = roleInfos.FirstOrDefault();
            CurrentDBSession.RoleInfoDal.DeleteEntity(roleInfo);
            return CurrentDBSession.SaveChanges();
        }

        public bool AddAction(int rid, string[] aidList)
        {
            var roleInfo = CurrentDal.LoadEntites(r=>r.ID==rid).FirstOrDefault();
            if (roleInfo == null)
            {
                return false;
            }
            roleInfo.ActionInfo.Clear();
            foreach (string aid in aidList)
            {
                ActionInfo actionInfo = CurrentDBSession.ActionInfoDal.LoadEntites(a => a.ID == int.Parse(aid)).FirstOrDefault();
                roleInfo.ActionInfo.Add(actionInfo);
            }

            return CurrentDBSession.SaveChanges();
        }

        public bool EditRoleAction(int rid, int aid, bool isPass)
        {
            RoleInfo roleInfo = CurrentDal.LoadEntites(r=>r.ID==rid).FirstOrDefault();
            if (roleInfo == null)
            {
                return false;
            }


            ActionInfo dbActionInfo = CurrentDBSession.ActionInfoDal.LoadEntites(a=>a.ID == aid).FirstOrDefault();

            if (isPass)
            {
                roleInfo.ActionInfo.Add(dbActionInfo);
            }
            else
            {
                roleInfo.ActionInfo.Remove(dbActionInfo);
            }
            
            return CurrentDBSession.SaveChanges();
        }
    }
}
