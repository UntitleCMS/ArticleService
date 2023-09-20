using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Article.Command.SavaArticle;

using CommandType = IRequest<IResponseWrapper<string>>;

public record SaveArticleCommand(string articleId, string sub) : CommandType;
