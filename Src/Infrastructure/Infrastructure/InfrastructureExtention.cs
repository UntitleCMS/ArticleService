
using Application.Common.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace Infrastructure;

public static class InfrastructureExtention
{ 
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        //services.AddSingleton<IMongoClient>(sp => new MongoClient("mongodb://localhost:27017"));
        services.AddSingleton<IMongoClient>(sp => {
            var settings = MongoClientSettings.FromConnectionString("mongodb://localhost:27017");
            settings.LoggingSettings = new LoggingSettings(sp.GetService<ILoggerFactory>());
            return new MongoClient(settings);
        });

        services.AddScoped<IPostRepository, PostRepository>();
        return services;
    }
}
