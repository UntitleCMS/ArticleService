using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Features.Article.Query.GetArticle;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>),typeof(ExceptionTransformPipelineBehevior<,>));

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
