using Domain.Entites;

namespace Application.Common.Repositories;

public interface IPostRepository
{
    Task SavePost(ref PostEntity post, CancellationToken cancellationToken = default);
    Task Update(PostEntity post, CancellationToken cancellationToken = default);
    Task<PostEntity> FindById(string id, string? sub = default, CancellationToken cancellationToken = default);
    Task Delete(string id, string? sub = default, CancellationToken cancellationToken = default);
    Task Like(string id, string sub, CancellationToken cancellationToken = default);
    Task UnLike(string id, string sub, CancellationToken cancellationToken = default);
    Task Save(string id, string sub, CancellationToken cancellationToken = default);
    Task UnSave(string id, string sub, CancellationToken cancellationToken = default);
}
