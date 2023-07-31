using Domain.Common;
using System.Linq.Expressions;

namespace Application.Common.Interfaces.Repositoris;

public interface IRepository<TEntity, TID>
    : ISaveable, IQueryable<TEntity>, IDisposable
    where TEntity : BaseEntity<TID>
    where TID : IEquatable<TID>
{
    TEntity? Find(TID id);
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
