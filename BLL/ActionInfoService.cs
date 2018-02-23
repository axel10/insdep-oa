using System;
using System.Linq;
using IBLL;
using Model;

namespace BLL
{
    public partial class ActionInfoService :BaseService<ActionInfo>,IActionInfoService
    {
        public bool EditAction(ActionInfo actionInfo)
        {
            ActionInfo dbAction = CurrentDal.LoadEntites(u => u.ID == actionInfo.ID).FirstOrDefault();
            if (dbAction == null)
            {
                return false;
            }
            dbAction.ActionInfoName = actionInfo.ActionInfoName;
            dbAction.Url = actionInfo.Url;
            dbAction.HttpMethod = actionInfo.HttpMethod;
            dbAction.Remark = actionInfo.Remark ?? "";
            dbAction.ModifiedOn = DateTime.Now;
            return CurrentDBSession.SaveChanges();
        }

        public bool DeleteActionById(int id)
        {
            var actionInfos = CurrentDBSession.ActionInfoDal.LoadEntites(a => a.ID == id);
            ActionInfo actionInfo = actionInfos.FirstOrDefault();
            CurrentDBSession.ActionInfoDal.DeleteEntity(actionInfo);
            return CurrentDBSession.SaveChanges();
        }

    }
}