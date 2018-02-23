 
using DAL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALFactory
{
	public partial class DBSession : IDBSession
    {
	
		private IActionInfoDal _ActionInfoDal;
        public IActionInfoDal ActionInfoDal
        {
            get
            {
                if(_ActionInfoDal == null)
                {
                    _ActionInfoDal = AbstractFactory.CreateActionInfoDal();
                }
                return _ActionInfoDal;
            }
            set { _ActionInfoDal = value; }
        }
	
		private IDepartmentDal _DepartmentDal;
        public IDepartmentDal DepartmentDal
        {
            get
            {
                if(_DepartmentDal == null)
                {
                    _DepartmentDal = AbstractFactory.CreateDepartmentDal();
                }
                return _DepartmentDal;
            }
            set { _DepartmentDal = value; }
        }
	
		private IMailInfoDal _MailInfoDal;
        public IMailInfoDal MailInfoDal
        {
            get
            {
                if(_MailInfoDal == null)
                {
                    _MailInfoDal = AbstractFactory.CreateMailInfoDal();
                }
                return _MailInfoDal;
            }
            set { _MailInfoDal = value; }
        }
	
		private IMailUserInfoDal _MailUserInfoDal;
        public IMailUserInfoDal MailUserInfoDal
        {
            get
            {
                if(_MailUserInfoDal == null)
                {
                    _MailUserInfoDal = AbstractFactory.CreateMailUserInfoDal();
                }
                return _MailUserInfoDal;
            }
            set { _MailUserInfoDal = value; }
        }
	
		private IR_UserInfo_ActionInfoDal _R_UserInfo_ActionInfoDal;
        public IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal
        {
            get
            {
                if(_R_UserInfo_ActionInfoDal == null)
                {
                    _R_UserInfo_ActionInfoDal = AbstractFactory.CreateR_UserInfo_ActionInfoDal();
                }
                return _R_UserInfo_ActionInfoDal;
            }
            set { _R_UserInfo_ActionInfoDal = value; }
        }
	
		private IRoleInfoDal _RoleInfoDal;
        public IRoleInfoDal RoleInfoDal
        {
            get
            {
                if(_RoleInfoDal == null)
                {
                    _RoleInfoDal = AbstractFactory.CreateRoleInfoDal();
                }
                return _RoleInfoDal;
            }
            set { _RoleInfoDal = value; }
        }
	
		private Itb_itemDal _tb_itemDal;
        public Itb_itemDal tb_itemDal
        {
            get
            {
                if(_tb_itemDal == null)
                {
                    _tb_itemDal = AbstractFactory.Createtb_itemDal();
                }
                return _tb_itemDal;
            }
            set { _tb_itemDal = value; }
        }
	
		private IUserInfoDal _UserInfoDal;
        public IUserInfoDal UserInfoDal
        {
            get
            {
                if(_UserInfoDal == null)
                {
                    _UserInfoDal = AbstractFactory.CreateUserInfoDal();
                }
                return _UserInfoDal;
            }
            set { _UserInfoDal = value; }
        }
	}	
}