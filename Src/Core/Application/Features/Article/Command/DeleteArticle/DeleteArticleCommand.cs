using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Article.Command.DeleteArticle;

using CommandType = IRequest<IResponseWrapper<string>>;

public record DeleteArticleCommand(string articleId, string? sub = default) : CommandType;
