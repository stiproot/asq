using dbaccess.Models;
using dbaccess.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace dbaccess.Repository
{
    public class GenericRespository<T> : IGenericRepository<T> where T : class
    {
        private readonly IAsqDbContextFactory<ASQContext> _contextFactory;

        private ASQContext _context()
        {
            var context = this._contextFactory.CreateContext();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return context;
        }

        public GenericRespository(IAsqDbContextFactory<ASQContext> contextFactory)
            => this._contextFactory = contextFactory;

        private IQueryable<T> Find(
            ASQContext context,
            Expression<Func<T, bool>> predicate, 
            IEnumerable<string> include = null, 
            Expression<Func<T, object>> orderByFunc = null,
            Expression<Func<T, object>> orderByDescFunc = null, 
            int? take = null)
        {
            if(orderByFunc != null && orderByDescFunc != null) throw new ArgumentException("Both order by and order by descending functions cannot be provided");

            IQueryable<T> qry = context.Set<T>().AsQueryable();
        
            if(orderByFunc != null) qry = qry.OrderBy(orderByFunc).AsQueryable();
            if(orderByDescFunc!= null) qry = qry.OrderByDescending(orderByDescFunc).AsQueryable();
            if(include != null) include?.ToList().ForEach(i => qry = qry.Include(i));
            if(predicate != null) qry = qry.Where(predicate);
            if(take != null) qry = qry.Take((int)take);

            return qry;
        }

        public async Task<IEnumerable<T>> FindToListAsync(
            Expression<Func<T, bool>> predicate, 
            IEnumerable<string> include = null, 
            Expression<Func<T, object>> orderByFunc = null,
            Expression<Func<T, object>> orderByDescFunc = null, 
            int? take = null)
        {
            if(orderByFunc != null && orderByDescFunc != null) throw new ArgumentException("Both order by and order by descending functions cannot be provided");

            using(var _c = this._context())
            {
                var qry = this.Find(
                    _c, 
                    predicate,
                    include,
                    orderByFunc,
                    orderByDescFunc);

                return await qry.ToListAsync();
            }
        }

        public async Task<T> FindFirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate, 
            IEnumerable<string> include = null, 
            Expression<Func<T, object>> orderByFunc = null,
            Expression<Func<T, object>> orderByDescFunc = null, 
            int? take = null)
        {
            if(orderByFunc != null && orderByDescFunc != null) throw new ArgumentException("Both order by and order by descending functions cannot be provided");

            using(var _c = this._context())
            {
                var qry = this.Find(
                    _c, 
                    predicate,
                    include,
                    orderByFunc,
                    orderByDescFunc);

                return await qry.FirstOrDefaultAsync();
            }
        }

        public async Task<T> FindSingleOrDefaultAsync(
            Expression<Func<T, bool>> predicate, 
            IEnumerable<string> include = null, 
            Expression<Func<T, object>> orderByFunc = null,
            Expression<Func<T, object>> orderByDescFunc = null, 
            int? take = null)
        {
            if(orderByFunc != null && orderByDescFunc != null) throw new ArgumentException("Both order by and order by descending functions cannot be provided");

            using(var _c = this._context())
            {
                var qry = this.Find(
                    _c, 
                    predicate,
                    include,
                    orderByFunc,
                    orderByDescFunc);

                return await qry.FirstOrDefaultAsync();
            }
        }

        public async Task<T> FindByKey(object id)
        {
            using(var _c = this._context())
            {
                DbSet<T> dbSet = _c.Set<T>();
                T ent = await dbSet.FindAsync(id);
                return ent;
            }
        }

        public async Task<IEnumerable<T>> All()
        {
            // "does not track any of the returned entities" - keep an eye on this... does it include linked entities.
            //return await _dbSet.AsNoTracking<T>().ToListAsync();

            using(var _c = this._context())
            {
                DbSet<T> dbSet = _c.Set<T>();
                return await dbSet.ToListAsync();
            }
        }

        public async Task<IEnumerable<T>> All(
            IEnumerable<string> include = null, 
            Expression<Func<T, object>> orderByFunc = null,
            Expression<Func<T, object>> orderByDescFunc = null
        )
        {
            // "does not track any of the returned entities" - keep an eye on this... does it include linked entities.

            using(var c = this._context())
            {
                return await this.Find(c, null, include, orderByFunc, orderByDescFunc).ToListAsync();
            }
        }

        public async Task Update(T entity)
        {
            using(var _c = this._context())
            {
                if(_c.Entry<T>(entity).State == EntityState.Detached)
                {
                    _c.Attach(entity);
                }

                _c.Entry<T>(entity).State = EntityState.Modified;
                await _c.SaveChangesAsync();
            }
        }

        public async Task Update(IEnumerable<T> entities)
        {
            using(var _c = this._context())
            {
                foreach(var entity in entities)
                {
                    if(_c.Entry<T>(entity).State == EntityState.Detached)
                    {
                        _c.Attach(entity);
                    }

                    _c.Entry<T>(entity).State = EntityState.Modified;
                }

                await _c.SaveChangesAsync();
            }
        }

        public async Task<T> Add(T entity)
        {
            using(var _c = this._context())
            {
                _c.Add(entity);
                await _c.SaveChangesAsync();
                return entity;
            }
        }

        public async Task Add(IEnumerable<T> entities)
        {
            using(var _c = this._context())
            {
                foreach(var entity in entities)
                {
                    _c.Add(entity);
                }
                await _c.SaveChangesAsync();
            }
        }

        public async Task Delete(T entity)
        {
            using(var _c = this._context())
            {
                DbSet<T> dbSet = _c.Set<T>();
                dbSet.Remove(entity);
                await _c.SaveChangesAsync();
            }
        }

        public async Task Delete(IEnumerable<T> entities)
        {
            using(var _c = this._context())
            {
                DbSet<T> dbSet = _c.Set<T>();
                foreach(var entity in entities)
                {
                    dbSet.Remove(entity);
                }
                await _c.SaveChangesAsync();
            }
        }

        //private IEnumerable<T> FromSqlRaw(string sql, object[] args)
        //{
            //using(var _c = this._context())
            //{
                //DbSet<T> dbSet = _c.Set<T>();
                //return dbSet.FromSqlRaw<T>(sql, args).IgnoreQueryFilters().AsEnumerable<T>();
            //}
        //}

        public async Task<T> FromSqlRawFirstOrDefaultAsync(string sql, object[] args)
        {
            using(var _c = this._context())
            {
                DbSet<T> dbSet = _c.Set<T>();
                var result =  dbSet.FromSqlRaw<T>(sql, args).IgnoreQueryFilters().AsEnumerable();
                return await Task.FromResult<T>(result.FirstOrDefault());
            }
        }
    }
}