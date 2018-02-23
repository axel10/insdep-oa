using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Search;

namespace IBLL
{
    public partial interface IUserInfoService:IBaseService<UserInfo>
    {
        bool DeleteEntities(List<int> list);
//        IQueryable<UserInfo> LoadSearchEntites(UserInfoSearch userInfoSearch,short delFlag);
        bool SetUserRoleInfo(int userId, List<int> roleIdList);
        bool SetUserActionInfo(int actionId, int userId, bool isPass);

        bool EditUser(UserInfo userInfo);
        bool DeleteUserById(int id);
        bool EditUserRole(int rid, int uid, bool isPass);
    }
}
