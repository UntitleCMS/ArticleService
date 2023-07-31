using Domain.Entity;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class DataContext
{
    private readonly MongoClient mongoClient;
    private readonly IMongoDatabase database;

    public DataContext()
    {
        var settings = MongoClientSettings.FromConnectionString("mongodb://mongo1:50001/?replicaSet=my-mongo-set");
        settings.LoggingSettings = new LoggingSettings();

        mongoClient = new MongoClient(settings);
        database = mongoClient.GetDatabase("post_service");
    }

    public Task<IClientSessionHandle> Session(CancellationToken cancellation = default)
        => mongoClient.StartSessionAsync(null, cancellation);

    public IMongoCollection<T> Collection<T>() where T : class
        => database.GetCollection<T>(typeof(T).Name.ToLower());
}
