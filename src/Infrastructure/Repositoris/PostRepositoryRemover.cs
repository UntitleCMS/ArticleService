
using Application.Common.Interfaces.Repositoris;
using Domain.Entity;
using Infrastructure.Collections;
using Infrastructure.Persistence;
using MongoDB.Driver;

namespace Infrastructure.Repositoris;

public class PostRepositoryRemover : IRepositoryRemover<Post, Guid>
{
    private readonly DataContext _context;
    private readonly IMongoCollection<PostCollection> _posts;

    public PostRepositoryRemover(DataContext context)
    {
        _context = context;
        _posts = _context.Collection<PostCollection>();
    }

    public void DeleteWithAuthority(Guid id, Guid authorId)
    {
        var res
            = _posts.FindOneAndDelete( p=>p.Id == id && p.AuthorId == authorId)
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
