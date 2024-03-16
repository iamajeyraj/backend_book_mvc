using System;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IDataService;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository {

    public class DataService<T> : IDataService<T> where T : class {
        protected ApplicationDbContext dbContext;
        internal DbSet<T> dbSet;

        public DataService(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Set<T>();
        }

        public void Add(T entity) {
            dbSet.Add(entity);
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter) {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public List<T> GetAll() {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public void Remove(T entity) {
            dbSet.Remove(entity);
        }

        public void Remove(IEnumerable<T> values) {
            dbSet.RemoveRange(values);
        }
    }
}

