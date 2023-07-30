using Application.Common.Extentions;
using Application.Common.Interfaces.Repositoris;
using Domain.Entity;
using Infrastructure.Persistence;
using MongoDB.Driver;
using System.Collections;
using System.Linq.Expressions;

namespace Infrastructure.Repositoris;

public class PostsRepository : IRepository<Post,Guid>
{
    private readonly DataContextContext _mongo;
    private readonly IMongoCollection<Post> _postsCol;
    private readonly IQueryable<Post> _postsQ;

    private IClientSessionHandle? _session;

    public PostsRepository(DataContextContext mongo)
    {
        _mongo = mongo;
        _postsCol = _mongo.Collection<Post>();
        _postsQ = _postsCol.AsQueryable();
    }

    public Expression Expression => _postsQ.Expression;

    public IQueryProvider Provider => _postsQ.Provider;

    public Type ElementType => typeof(Post);

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

    public void Add(Post entity)
    {
        _postsCol.InsertOne(Session, entity);
    }

    public void AddRange(IEnumerable<Post> entities)
    {
        _postsCol.InsertMany(entities);
    }

    public void Dispose()
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

    public Post Find(Guid id)
    {
        var p =  _postsCol.Find( p=>p.ID==id )
            .FirstOrDefault();
        return p;
    }

    public ValueTask<Post> FindAsync(Guid id)
    {
        return new ValueTask<Post>( _postsCol.FindAsync(p => p.ID == id).Result.Single());
    }

    public IEnumerator<Post> GetEnumerator()
    {
        return _postsQ.GetEnumerator();
    }

    public void Remove(Post entity)
    {
        _postsCol.FindOneAndDelete(p => p == entity);
    }

    public void RemoveById(Guid entityId)
    {
        _postsCol.FindOneAndDelete(p => p.ID == entityId);
    }

    public void RemoveRange(IEnumerable<Post> entities)
    {
        _postsCol.DeleteMany(p => entities.Contains(p));
    }

    public void RemoveRange(Expression<Func<Post, bool>> predicate)
    {
        _postsCol.DeleteMany(predicate);
    }

    public void SaveChanges()
    {
        Session.CommitTransaction();
        _session = null;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Session.CommitTransactionAsync(cancellationToken);
    }

    public void Update(Post entity)
    {
        var res = _postsCol.ReplaceOne(Session, p =>
            p.ID == entity.ID &&
            p.Author == entity.Author,
            entity);
        if (res.ModifiedCount == 0)
            throw new Exception($"Post id {entity.ID.ToBase64Url()} of {entity.Author.ToBase64Url()} not found.");
    }

    public void UpdateRange(IEnumerable<Post> entities)
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
