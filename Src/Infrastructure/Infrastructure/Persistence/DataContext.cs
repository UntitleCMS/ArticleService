using MongoDB.Driver;

namespace Infrastructure.Persistence;

public class DataContext
{
    private readonly IMongoClient mongoClient;
    private readonly IMongoDatabase database;

    public DataContext()
    {
        var settings = MongoClientSettings
            .FromConnectionString("mongodb://mongo1:50001/?replicaSet=my-mongo-set");

        mongoClient = new MongoClient(settings);
        database = mongoClient.GetDatabase("post_service");
    }

    public Task<IClientSessionHandle> Session(CancellationToken cancellation = default)
        => mongoClient.StartSessionAsync(null, cancellation);

    public IMongoCollection<T> Collection<T>() where T : class
        => database.GetCollection<T>(typeof(T).Name.ToLower());
}
