using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;

public interface IAppMongoDbContext: ISaveable
{
    // Collections
    IDbCollectionSet<Post> Posts { get; }
    IDbCollectionSet<Tag> Tags { get; }

    // Tools
    IQueryable<TEntity> Query<TEntity>() where TEntity : class;
    void Attach<TEntity>(TEntity entity) where TEntity : class;
    void AttachRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
}
