using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using IDAL;

namespace DALFactory
{
    public partial class DBSession:IDBSession
    {
        public DbContext Db
        {
            get { return DBContextFactory.CreateDbContext(); }
        }



        public bool SaveChanges()
        {
            bool b = Db.SaveChanges() > 0;
            return b;
        }
    }
}
