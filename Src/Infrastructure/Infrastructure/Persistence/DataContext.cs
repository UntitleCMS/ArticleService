using MongoDB.Driver;
using System.Security.Authentication;
using System.Text.RegularExpressions;

namespace Infrastructure.Persistence;

public class DataContext
{
    private readonly IMongoClient mongoClient;
    private readonly IMongoDatabase database;

    public DataContext(string connectionString)
    {
        var settings = MongoClientSettings
            .FromUrl(new MongoUrl(connectionString));

        if (connectionString.Contains("ssl=true",StringComparison.OrdinalIgnoreCase))
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

        mongoClient = new MongoClient(settings);
        database = mongoClient.GetDatabase("articles");
    }

    public Task<IClientSessionHandle> Session(CancellationToken cancellation = default)
        => mongoClient.StartSessionAsync(null, cancellation);

    public IMongoCollection<T> Collection<T>() where T : class
        => database.GetCollection<T>(typeof(T).Name.ToLower());
}
