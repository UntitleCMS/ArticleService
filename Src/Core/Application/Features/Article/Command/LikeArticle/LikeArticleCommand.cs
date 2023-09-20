using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Article.Command.LikeArticle;

using CommandType = IRequest<IResponseWrapper<string>>;

public record LikeArticleCommand(string articleId, string sub) : CommandType;
