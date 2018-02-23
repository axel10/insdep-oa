using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using Model;

namespace DAL
{
    public class DBContextFactory
    {
        public static DbContext CreateDbContext()
        {
            DbContext dbContext = (DbContext) CallContext.GetData("dbContext");
            if (dbContext == null)
            {
                dbContext = new OAEntities();
                CallContext.SetData("dbContext", dbContext);
            }

            return dbContext;
        }
    }
}