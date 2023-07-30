
using Application.Common.Interfaces.Repositoris;
using Domain.Entity;
using Infrastructure.Persistence;
using MongoDB.Driver;

namespace Infrastructure.Repositoris;

public class PostRepositoryRemover : IRepositoryRemover<Post, Guid>
{
    private DataContextContext _context;
    private IMongoCollection<Post> _posts;

    public PostRepositoryRemover(DataContextContext context)
    {
        _context = context;
        _posts = _context.Collection<Post>();
    }

    public void DeleteWithAuthority(Guid id, Guid authorId)
    {
        var res
            = _posts.FindOneAndDelete( p=>p.ID == id && p.Author == authorId)
            ?? throw new Exception($"Id {id} of author {authorId} not found.");
    }

    public void Dispose()
    {
    }

    public void SaveChanges()
    {
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
