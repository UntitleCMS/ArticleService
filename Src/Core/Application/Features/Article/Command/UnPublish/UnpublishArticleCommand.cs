using Application.Common.Mediator;

namespace Application.Features.Article.Command.UnPublish;

public record UnpublishArticleCommand(string id, string sub) : IRequestWraped<string>;
