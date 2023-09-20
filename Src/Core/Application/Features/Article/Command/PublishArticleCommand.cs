using Application.Common.Mediator;

namespace Application.Features.Article.Command;

using ResponseType = String;

public record PublishArticleCommand(string id, string sub) : IRequestWraped<ResponseType>;
