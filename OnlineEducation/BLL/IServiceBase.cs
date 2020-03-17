using System;
using OnlineEducation.DAL;
using OnlineEducation.Common;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace OnlineEducation.BLL
{
    public interface IServiceBase<TEntity>
        where TEntity : class, IEntity
    {
        Task<bool> Any(int id);
        Task<bool> Any(Expression<Func<TEntity, bool>> expression);
        Task<int> Count();
        Task<int> Count(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> Add(TEntity newModel);
        Task<TEntity> Get(int id);
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> expression);
        Task<FilterResult<TEntity>> Get(Expression<Func<TEntity, bool>> expression, int start, int count);
        Task Remove(int id);
        Task<TEntity> Update(TEntity newModel);
    }
}