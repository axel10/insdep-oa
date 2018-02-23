using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IDAL;

namespace DALFactory
{
    public partial class AbstractFactory
    {
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
        private static readonly string NameSpace = ConfigurationManager.AppSettings["NameSpace"];

        public static IUserInfoDal CreateUserDal()
        {
            string fullClassName = NameSpace + ".UserInfoDal";
            return CreateInstance(fullClassName) as IUserInfoDal;
        }

        private static object CreateInstance(string fullClassName)
        {
            var assembly = Assembly.Load(AssemblyPath);
            return assembly.CreateInstance(fullClassName);
        }
    }
}
