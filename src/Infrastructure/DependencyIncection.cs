using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositoris;
using Domain.Entity;
using Infrastructure.Persistence;
using Infrastructure.Repositoris;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

public static class DependencyIncection
{
    private static readonly InfrastructureOptionBuilder OptionsBuilder = new();
    private static InfrastructureOption Options { get { return OptionsBuilder.Build(); } }

    public static void AddInfrastructure(this IServiceCollection service, Action<InfrastructureOptionBuilder> option)
    {
        option(OptionsBuilder);

        //service.AddDbContext<AppDbContext>(options =>
        //{
        //    options.UseSqlServer
        //    (
        //        Options.DbConnectionString,
        //        opt=> opt.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
        //    );
        //});

        //service.AddScoped<IAppDbContext>( 
        //    sp => sp.GetRequiredService<AppDbContext>() );

        //service.BuildServiceProvider()
        //    .GetRequiredService<AppDbContext>()
        //    .Database
        //    .EnsureCreated();

        // Add MongoDb
        service.AddSingleton<IAppMongoDbContext,AppMongoDbContext>();
        service.AddScoped<IRepository<Post, Guid>, PostsRepository>();
    }
}

public class InfrastructureOptionBuilder
{
    private readonly InfrastructureOption opt;
    public InfrastructureOptionBuilder()
    {
        opt = new InfrastructureOption();
    }

    public InfrastructureOptionBuilder AddDbConnectionString(string? dbConnectionString)
    { 
        opt.DbConnectionString = dbConnectionString;
        return this;
    }

    public InfrastructureOptionBuilder AddDbMigrationAssembly(string? dbMigrationAssembly)
    { 
        opt.DbMigrationAssembly = dbMigrationAssembly;
        return this;
    }


    public InfrastructureOption Build()
    {
        return opt;
    }
}
public class InfrastructureOption
{
    public string? DbConnectionString { get; set; }
    public string? DbMigrationAssembly { get; set; }
}
