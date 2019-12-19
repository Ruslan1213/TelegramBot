using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Introspect.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        IEnumerable<T> GetAll();

        T Get(int id);

        bool IsExist(Expression<Func<T, bool>> expression);
    }
}
