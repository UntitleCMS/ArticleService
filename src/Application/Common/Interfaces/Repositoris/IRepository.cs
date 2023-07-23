using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;

public interface IRepository<TEntity,TID> : ISaveable, IQueryable<TEntity>, IDisposable
    where TEntity : class
{
    TEntity Find(TID id);
    ValueTask<TEntity> FindAsync(TID id);
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    void RemoveRange(Expression<Func<TEntity, bool>> predicate);
    void RemoveById(TID entityId);
}
