using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class BaseDal<T> where T:class,new()
    {
        //        OAEntities db = new OAEntities();
        DbContext db = DAL.DBContextFactory.CreateDbContext();
        public IQueryable<T> LoadEntites(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where<T>(whereLambda);
        }

        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            var temp = db.Set<T>().Where<T>(whereLambda);
            totalCount = temp.Count();
            if (isAsc)
            {
                temp = temp.OrderBy<T, s>(orderbyLambda).Skip((pageIndex - 1) * pageSize).Take<T>(pageSize);

            }
            else
            {
                temp = temp.OrderByDescending<T,s>(orderbyLambda).Skip((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }

            return temp;
        }

        public bool DeleteEntity(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Deleted;
            return true;
        }

        public async Task Demo(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }

        public bool EditEntity(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public T AddEntity(T entity)
        {
            db.Set<T>().Add(entity);
            return entity;
        }
    }
}
