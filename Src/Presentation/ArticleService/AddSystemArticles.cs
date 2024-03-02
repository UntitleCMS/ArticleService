
using Application.Common.Repositories;
using Microsoft.OpenApi.Writers;

namespace ArticleService;

public class AddSystemArticles : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public AddSystemArticles(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var articleRepository = scope.ServiceProvider.GetRequiredService<IDefaultArticleRepository>();

        articleRepository.AddTermOfServiceAndPolicy();
        articleRepository.AddSupportedLang();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
