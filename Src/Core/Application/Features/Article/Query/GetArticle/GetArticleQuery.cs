using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Article.Query.GetArticle;

using GetArticleQueryType =
    IRequest<IResponseWrapper<GetArticleQueryResponse>>;


public record GetArticleQuery(string ArticleId, string? sub = default) : GetArticleQueryType;

