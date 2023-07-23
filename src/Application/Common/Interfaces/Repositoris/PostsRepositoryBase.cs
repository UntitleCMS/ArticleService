using System.Collections;
using System.Linq.Expressions;

namespace Application.Common.Interfaces.Repositoris;

public abstract class PostsRepositoryBase<TEntity, TID> : IRepository<TEntity,TID>, IQueryable<TEntity>
    where TEntity : class
{
    public Type ElementType => typeof(TEntity);

    public abstract Expression Expression { get; }
    public abstract IQueryProvider Provider { get; }

    public abstract void Add(TEntity entity);
    public abstract void AddRange(IEnumerable<TEntity> entities);
    public abstract void Dispose();
    public abstract TEntity Find(TID id);
    public abstract ValueTask<TEntity> FindAsync(TID id);
    public abstract IEnumerator<TEntity> GetEnumerator();
    public abstract void Remove(TEntity entity);
    public abstract void RemoveById(TID entityId);
    public abstract void RemoveRange(IEnumerable<TEntity> entities);
    public abstract void RemoveRange(Expression<Func<TEntity, bool>> predicate);
    public abstract void SaveChanges();
    public abstract Task SaveChangesAsync(CancellationToken cancellationToken = default);
    public abstract void Update(TEntity entity);
    public abstract void UpdateRange(IEnumerable<TEntity> entities);

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
