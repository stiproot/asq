using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;

namespace dbaccess.Repository
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> FindToListAsync(
            Expression<Func<T, bool>> predicate, 
            IEnumerable<string> include = null, 
            Expression<Func<T, object>> orderByFunc = null,
            Expression<Func<T, object>> orderByDescFunc = null, 
            int? take = null);

        Task<T> FindFirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate, 
            IEnumerable<string> include = null, 
            Expression<Func<T, object>> orderByFunc = null,
            Expression<Func<T, object>> orderByDescFunc = null, 
            int? take = null);

        Task<T> FindSingleOrDefaultAsync(
            Expression<Func<T, bool>> predicate, 
            IEnumerable<string> include = null, 
            Expression<Func<T, object>> orderByFunc = null,
            Expression<Func<T, object>> orderByDescFunc = null, 
            int? take = null);

        Task<T> FindByKey(object id);

        Task<IEnumerable<T>> All();

        Task<IEnumerable<T>> All(
            IEnumerable<string> include = null, 
            Expression<Func<T, object>> orderByFunc = null,
            Expression<Func<T, object>> orderByDescFunc = null
        );

        Task Update(T entity);

        Task Update(IEnumerable<T> entities);

        Task<T> Add(T entity);

        Task Add(IEnumerable<T> entities);

        Task Delete(T entity);

        Task Delete(IEnumerable<T> entities);

        Task<T> FromSqlRawFirstOrDefaultAsync(string sql, object[] args);
    }
}
