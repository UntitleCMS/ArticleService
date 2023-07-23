using Application.Common.Interfaces.Repositoris;
using Application.Posts.Dto;
using Domain.Entity;
using Infrastructure.Persistence;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.Repositoris;

public class PostsRepository : PostsRepositoryBase<Post,Guid>
{
    private readonly DataContextContext _mongo;
    private readonly IMongoCollection<Post> _postsCol;
    private readonly IQueryable<Post> _postsQ;

    private IClientSessionHandle? _session;

    public PostsRepository()
    {
        _mongo = new();
        _postsCol = _mongo.Collection<Post>();
        _postsQ = _postsCol.AsQueryable();
    }

    public override Expression Expression => _postsQ.Expression;

    public override IQueryProvider Provider => _postsQ.Provider;

    private IClientSessionHandle Session
    {
        get
        {
            _session ??= _mongo.Session().Result;
            if (!_session.IsInTransaction)
                _session.StartTransaction();
            return _session;
        }
    }

    public override void Add(Post entity)
    {
        _postsCol.InsertOne(Session, entity);
    }

    public override void AddRange(IEnumerable<Post> entities)
    {
        _postsCol.InsertMany(entities);
    }

    public override void Dispose()
    {
        Console.WriteLine("Post Repository is Dispose.");
        if (_session is not null && _session.IsInTransaction)
        {
            Console.WriteLine("Post Repository is Abort Transaction.");
            _session.AbortTransaction();
        }
        _session?.Dispose();
        _session = null;
    }

    public override Post Find(Guid id)
    {
        return _postsQ.Single(p=>p.ID == id);
    }

    public override  ValueTask<Post> FindAsync(Guid id)
    {
        return new ValueTask<Post>( _postsCol.FindAsync(p => p.ID == id).Result.Single());
    }

    public override IEnumerator<Post> GetEnumerator()
    {
        return _postsQ.GetEnumerator();
    }

    public override void Remove(Post entity)
    {
        _postsCol.FindOneAndDelete(p => p.ID == entity.ID);
    }

    public override void RemoveById(Guid entityId)
    {
        _postsCol.FindOneAndDelete(p => p.ID == entityId);
    }

    public override void RemoveRange(IEnumerable<Post> entities)
    {
        throw new NotImplementedException();
    }

    public override void RemoveRange(Expression<Func<Post, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public override void SaveChanges()
    {
        Session.CommitTransaction();
    }

    public override Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Session.CommitTransactionAsync(cancellationToken);
    }

    public override void Update(Post entity)
    {
        throw new NotImplementedException();
    }

    public override void UpdateRange(IEnumerable<Post> entities)
    {
        throw new NotImplementedException();
    }
}
