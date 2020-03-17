using System;
using System.Linq;
using OnlineEducation.DAL;
using OnlineEducation.Common;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OnlineEducation.BLL
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity>
        where TEntity: class, IEntity
    {
        protected OnlineEducationDbContext _db;
        protected DbSet<TEntity> _dbSet;

        public ServiceBase(OnlineEducationDbContext dbContext)
        {
            _db = dbContext;
            _dbSet = _db.Set<TEntity>();
        }

        public virtual async Task<bool> Any(int id)
        {
            return await _dbSet.AnyAsync(x => x.Id == id);
        }

        public virtual async Task<int> Count()
        {
            return await _dbSet.CountAsync();
        }

        public virtual async Task<TEntity> Add(TEntity newModel)
        {
            var data = await _dbSet.AddAsync(newModel);
            await _db.SaveChangesAsync();
            var newEntity = data.Entity;
            return newEntity;
        }

        public virtual async Task<TEntity> Get(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public virtual async Task<FilterResult<TEntity>> Get(Expression<Func<TEntity, bool>> expression, int start, int count)
        {
            var allCount = await _dbSet.CountAsync(expression);

            var query = _dbSet
                .Where(expression)
                .OrderByDescending(x => x.Id)
                .Skip(start).Take(count);

            var entityList = await query.ToListAsync();

            var filterResult = new FilterResult<TEntity> { Data = entityList, ItemsCount = allCount };

            return filterResult;
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            var entityList = await _dbSet.ToListAsync();
            return entityList;
        }

        public virtual async Task Remove(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _ = _dbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public virtual async Task<TEntity> Update(TEntity newModel)
        {
            var data = _dbSet.Update(newModel);
            await _db.SaveChangesAsync();

            var updatedEntity = data.Entity;
            return updatedEntity;
        }

        public virtual async Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> expression)
        {
            var query = _dbSet.Where(expression);
            var entityList = await query.ToListAsync();
            return entityList;
        }

        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.CountAsync(expression);
        }

        public virtual async Task<bool> Any(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }
    }
}
