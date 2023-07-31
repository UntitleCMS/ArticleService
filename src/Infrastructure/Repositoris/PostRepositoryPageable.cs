using Application.Common.Interfaces.Repositoris;
using Domain.Entity;
using Infrastructure.Collections;
using Infrastructure.Common.Mappers;
using Infrastructure.Persistence;
using MongoDB.Driver;

namespace Infrastructure.Repositoris;

public class PostRepositoryPageable : IRepositoryPageable<Post, Guid>
{
    private readonly DataContext _context;
    private readonly IMongoCollection<PostCollection> _posts;

    public PostRepositoryPageable(DataContext ctx)
    { 
        _context = ctx;
        _posts = _context.Collection<PostCollection>();
    }
    public void Dispose() { }

    public IEnumerable<Post> GetAfter(int range, Guid? referance = null)
    {
        var dateref = DateTime.Now;

        if (referance is not null)
        {
            var cutoffDate = _posts
                .Find(p => p.Id == referance)
                .FirstOrDefault();

            if (cutoffDate is null)
                return Enumerable.Empty<Post>();

            dateref = cutoffDate.CreatedAt;
        }

        var filter = Builders<PostCollection>.Filter.Gt(e => e.CreatedAt, dateref);
        var sort = Builders<PostCollection>.Sort.Ascending(e => e.CreatedAt);

        var entities = _posts
            .Find(filter)
            .Sort(sort)
            .Limit(range+1)
            .ToList()
            .Select(p => p.ToPost());

        return entities;
    }

    public IEnumerable<Post> GetBefore(int range, Guid? referance = null)
    {
        var dateref = DateTime.Now;

        if (referance is not null)
        {
            var cutoffDate = _posts
                .Find(p => p.Id == referance)
                .FirstOrDefault();

            if (cutoffDate is null)
                return Enumerable.Empty<Post>();

            dateref = cutoffDate.CreatedAt;
        }

        var filter = Builders<PostCollection>.Filter.Lt(e => e.CreatedAt, dateref);
        var sort = Builders<PostCollection>.Sort.Descending(e => e.CreatedAt);
        var entities = _posts
            .Find(filter)
            .Sort(sort)
            .Limit(range+1)
            .ToList()
            .Select(p =>p.ToPost());

        return entities;
    }
}
