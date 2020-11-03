using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SEBtask.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public RepositoryContext RepositoryContext;

        public BaseRepository(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll()
        {
            return RepositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindAllByCondition(Expression<Func<T, bool>> expresion)
        {
            return RepositoryContext.Set<T>().Where(expresion).AsNoTracking();
        }

        public T FindFirstByCondition(Expression<Func<T, bool>> expresion)
        {
            return RepositoryContext.Set<T>().AsNoTracking().FirstOrDefault(expresion);
        }

        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }
    }
}
