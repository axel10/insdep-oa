using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DALFactory;
using IDAL;

namespace BLL
{
    public abstract class BaseService<T> where T:class,new()
    {
        public BaseService()
        {
            SetCurrentDal();
        }
        public IBaseDal<T> CurrentDal { get; set; }

        public IDBSession CurrentDBSession 
        {
            get { return DBSessionFactory.CreateDBSession(); }
        }

        public abstract void SetCurrentDal();

        public IQueryable<T> LoadEntites(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.LoadEntites(whereLambda);
        }

        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.LoadEntites(whereLambda);
        }

        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, s>> orderByLambda, bool isAsc)
        {
            return CurrentDal.LoadPageEntities<s>(pageIndex, pageSize, out totalCount, whereLambda, orderByLambda,
                isAsc);
        }

        public bool DeleteEntity(T entity)
        {
            CurrentDal.DeleteEntity(entity);
            return CurrentDBSession.SaveChanges();
        }

        public bool EditEntity(T entity)
        {
            CurrentDal.EditEntity(entity);
            return CurrentDBSession.SaveChanges();
        }


        public T AddEntity(T entity)
        {
            CurrentDal.AddEntity(entity);
            bool b = CurrentDBSession.SaveChanges();
            if (!b)
            {
                throw new Exception("sql语句执行失败");
            }
            return entity;
        }

        

    }
}
