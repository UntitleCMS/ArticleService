using Application.Common.Extentions;
using Application.Common.Interfaces.Repositoris;
using Domain.Entity;
using Infrastructure.Collections;
using Infrastructure.Common.Mappers;
using Infrastructure.Persistence;
using MongoDB.Driver;
using System.Collections;
using System.Linq.Expressions;

namespace Infrastructure.Repositoris;

public class PostsRepository : IRepository<Post,Guid>
{
    private readonly DataContext _mongo;
    private readonly IMongoCollection<PostCollection> _postsCol;
    private readonly IQueryable<PostCollection> _postsQ;

    private IClientSessionHandle? _session;

    public PostsRepository(DataContext mongo)
    {
        _mongo = mongo;
        _postsCol = _mongo.Collection<PostCollection>();
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
        var post = entity.ToPostCollection();
        _postsCol.InsertOne(Session, post);
        entity.ID = post.Id;
    }

    public void AddRange(IEnumerable<Post> entities)
    {
        _postsCol.InsertMany(entities.Select(p =>p.ToPostCollection()));
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
        var p =  _postsCol.Find( p=>p.Id==id )
            .FirstOrDefault();
        return p.ToPost();
    }

    public ValueTask<Post> FindAsync(Guid id)
    {
        return new ValueTask<Post>
        (
            _postsCol.FindAsync(p => p.Id == id)
            .Result
            .Single()
            .ToPost()
        );
    }

    public IEnumerator<Post> GetEnumerator()
    {
        return _postsQ.Select(p=>p.ToPost()).GetEnumerator();
    }

    public void Remove(Post entity)
    {
        _postsCol.FindOneAndDelete(p =>
            p.Id == entity.ID &&
            p.AuthorId==entity.Author);
    }

    public void RemoveById(Guid entityId)
    {
        _postsCol.FindOneAndDelete(p => p.Id == entityId);
    }

    public void RemoveRange(IEnumerable<Post> entities)
    {
        _postsCol.DeleteMany(p => entities.Any(q=>q.ID==p.Id && q.Author==p.AuthorId));
    }

    public void RemoveRange(Expression<Func<Post, bool>> predicate)
    {
        throw new NotImplementedException();
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
            p.Id == entity.ID &&
            p.AuthorId == entity.Author,
            entity.ToPostCollection());
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
