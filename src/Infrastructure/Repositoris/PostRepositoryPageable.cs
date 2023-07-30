using Application.Common.Interfaces.Repositoris;
using Domain.Entity;
using Infrastructure.Persistence;
using MongoDB.Driver;
using System.Xml;

namespace Infrastructure.Repositoris;

public class PostRepositoryPageable : IRepositoryPageable<Post, Guid>
{
    private readonly DataContextContext _context;

    public PostRepositoryPageable(DataContextContext ctx)
    { 
        _context = ctx;
    }
    public void Dispose() { }

    public IEnumerable<Post> GetAfter(int range, Guid? referance = null)
    {
        var dateref = DateTime.Now;

        if (referance is not null)
        {
            var cutoffDate = _context.Collection<Post>()
                .Find(p => p.ID == referance)
                .FirstOrDefault();

            if (cutoffDate is null)
                return Enumerable.Empty<Post>();

            dateref = cutoffDate.CreatedAt;
        }

        var filter = Builders<Post>.Filter.Gt(e => e.CreatedAt, dateref);
        var sort = Builders<Post>.Sort.Ascending(e => e.CreatedAt);
        var entities = _context.Collection<Post>()
            .Find(filter)
            .Sort(sort)
            .Limit(range+1)
            .ToList();

        return entities;
    }

    public IEnumerable<Post> GetBefore(int range, Guid? referance = null)
    {
        var dateref = DateTime.Now;

        if (referance is not null)
        {
            var cutoffDate = _context.Collection<Post>()
                .Find(p => p.ID == referance)
                .FirstOrDefault();

            if (cutoffDate is null)
                return Enumerable.Empty<Post>();

            dateref = cutoffDate.CreatedAt;
        }

        var filter = Builders<Post>.Filter.Lt(e => e.CreatedAt, dateref);
        var sort = Builders<Post>.Sort.Descending(e => e.CreatedAt);
        var entities = _context.Collection<Post>()
            .Find(filter)
            .Sort(sort)
            .Limit(range+1)
            .ToList();

        return entities;
    }
}
