using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IBLL
{
    public partial interface IRoleInfoService
    {
        bool EditRole(RoleInfo role);
        bool DeleteRoleById(int id);
        bool AddAction(int rid, string[] aidList);
        bool EditRoleAction(int rid, int aid, bool isPass);
    } 
}
