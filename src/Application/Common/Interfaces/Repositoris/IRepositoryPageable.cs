using Domain.Common;

namespace Application.Common.Interfaces.Repositoris;

public interface IRepositoryPageable<TEntity, TId> : IDisposable
    where TEntity : BaseEntity<TId>
    where TId : struct, IEquatable<TId>
{
    IEnumerable<TEntity> GetBefore(int range, TId? referance = null);
    IEnumerable<TEntity> GetAfter(int range, TId? referance = null);
}
