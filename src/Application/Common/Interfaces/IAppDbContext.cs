using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAppDbContext 
    {
        DbSet<Post> Posts { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Tag> Tags { get; set; }

        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity:class;
        EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity:class;

        //void RemoveRange(IEnumerable<object> entities);
        //EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity:class;
    } 
}
