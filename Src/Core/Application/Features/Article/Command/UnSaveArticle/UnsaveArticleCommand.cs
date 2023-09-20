using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Article.Command.UnSaveArticle;

using CommandType = IRequest<IResponseWrapper<string>>;

public record UnsaveArticleCommand(string articleId, string sub) : CommandType;
