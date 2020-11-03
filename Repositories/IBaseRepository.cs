using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SEBtask.Repositories
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindAllByCondition(Expression<Func<T, bool>> expresion);
        T FindFirstByCondition(Expression<Func<T, bool>> expresion);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
