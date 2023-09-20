using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Article.Command.UnlikeArticle;

using CommandType = IRequest<IResponseWrapper<string>>;

public record UnlikeArticleCommand(string articleId, string sub) : CommandType;
