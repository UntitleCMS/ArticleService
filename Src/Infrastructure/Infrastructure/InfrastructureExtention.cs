
using Application.Common.Repositories;
using Infrastructure.Queues;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using Application.Common.Queues;

namespace Infrastructure;

public static class InfrastructureExtention
{ 
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        //services.AddSingleton<IMongoClient>(sp => new MongoClient("mongodb://localhost:27017"));
        //services.AddSingleton<IMongoClient>(sp => {
        //    var settings = MongoClientSettings.FromConnectionString("mongodb://localhost:27017");
        //    settings.LoggingSettings = new LoggingSettings(sp.GetService<ILoggerFactory>());
        //    return new MongoClient(settings);
        //});

        services.AddSingleton(sp =>
        {
            string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                ?? throw new Exception("Not Found Enviroment Variable : 'DB_CONNECTION_STRING'");
            return new DataContext(connectionString);
        });

        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IArticlesPageableRepository, ArticlesPageableRepository>();
        services.AddScoped<IArticlesNameRepository, ArticlesNameRepository>();

        services.AddSingleton<INewArticleQueue, NewArticleBus>();
        return services;
    }
}
