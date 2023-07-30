
using Domain.Common;

namespace Application.Common.Interfaces.Repositoris;

public interface IRepositoryRemover<TEntity, TId> : IDisposable, ISaveable
    where TEntity : BaseEntity<TId>
    where TId : struct, IEquatable<TId>

{
    void DeleteWithAuthority(TId id, TId authorId);
}
