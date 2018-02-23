using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using IDAL;

namespace DALFactory
{
    public class DBSessionFactory
    {
        public static IDBSession CreateDBSession()
        {
            IDBSession dbSession = (IDBSession) CallContext.GetData("dbSession");
            if (dbSession == null)
            {
                dbSession = new DBSession();
                CallContext.SetData("dbSession",dbSession);
            }

            return dbSession;
        }
    }
}