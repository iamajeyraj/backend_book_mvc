using System;
using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository.IDataService
{
	public interface IDataService<T> where T :class
	{
        List<T> GetAll();
        T Get(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Remove(T entity);
        void Remove(IEnumerable<T> values);
    }
}

