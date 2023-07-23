using Amazon.Runtime.Internal.Util;
using Application.Common.Extentions;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entity;
using Microsoft.Extensions.Logging;
using MongoFramework;
using MongoFramework.Infrastructure.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class AppMongoDbContext : MongoDbContext, IAppMongoDbContext
{
    private readonly ILogger<AppMongoDbContext> _logger;    
    private static readonly IMongoDbConnection _connectionString 
        = MongoDbConnection.FromConnectionString("mongodb://user:pass@mongodb/admin");

    public MyMongoDbSet<Post> _Posts { get; set; }
    public MyMongoDbSet<Tag> _Tags { get; set; }
    public IDbCollectionSet<Post> Posts => _Posts;
    public IDbCollectionSet<Tag> Tags => _Tags;

    public AppMongoDbContext(ILogger<AppMongoDbContext> logger = null) : base(_connectionString)
    {
        _logger = logger;
        _logger.LogInformation("Mongo DB Context is create");
    }

}

public class MyMongoDbSet<TEntity> : MongoDbSet<TEntity>, IDbCollectionSet<TEntity> where TEntity : class
{
    public MyMongoDbSet(IMongoDbContext context) : base(context) { }
}
