 

using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
		
	public partial class ActionInfoDal :BaseDal<ActionInfo>,IActionInfoDal
    {

    }
		
	public partial class DepartmentDal :BaseDal<Department>,IDepartmentDal
    {

    }
		
	public partial class MailInfoDal :BaseDal<MailInfo>,IMailInfoDal
    {

    }
		
	public partial class MailUserInfoDal :BaseDal<MailUserInfo>,IMailUserInfoDal
    {

    }
		
	public partial class R_UserInfo_ActionInfoDal :BaseDal<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoDal
    {

    }
		
	public partial class RoleInfoDal :BaseDal<RoleInfo>,IRoleInfoDal
    {

    }
		
	public partial class tb_itemDal :BaseDal<tb_item>,Itb_itemDal
    {

    }
		
	public partial class UserInfoDal :BaseDal<UserInfo>,IUserInfoDal
    {

    }
	
}