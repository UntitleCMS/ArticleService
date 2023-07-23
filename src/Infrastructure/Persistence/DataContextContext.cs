using Domain.Entity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class DataContextContext
{
    private readonly MongoClient mongoClient;
    private readonly IMongoDatabase database;

    public DataContextContext()
    {
        mongoClient = new MongoClient("mongodb://mongo1:50001/?replicaSet=my-mongo-set");
        database = mongoClient.GetDatabase("post_service");
    }

    public Task<IClientSessionHandle> Session(CancellationToken cancellation = default)
        => mongoClient.StartSessionAsync(null, cancellation);

    public IMongoCollection<T> Collection<T>() where T : class
        => database.GetCollection<T>(typeof(T).Name.ToLower());
}
