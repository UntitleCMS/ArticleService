using Application.Common.Interfaces;
using Application.Common.models;
using Application.Common.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Article.Query.GetArticle;

using GetArticleQueryResponseType =
    IResponseWrapper<GetArticleQueryResponse>;

using GetArticleQueryHandlerType =
    IRequestHandler<GetArticleQuery, IResponseWrapper<GetArticleQueryResponse>>;

public class GetArticleQueryHandler : GetArticleQueryHandlerType
{
    private readonly IPostRepository _repo;

    public GetArticleQueryHandler(IPostRepository repo)
    {
        _repo = repo;
    }


    public async Task<GetArticleQueryResponseType> Handle(
        GetArticleQuery request, CancellationToken cancellationToken)
    {
        var result = _repo.FindById(request.ArticleId, request.sub);

        if (result.Exception is not null) return ResponseWrapper
           .Error<GetArticleQueryResponse>(result.Exception, "Not Found.");

        var article = result.Result;

        return ResponseWrapper.Ok(new GetArticleQueryResponse()
        {
            Id = Base64UrlEncoder.Encode(article.ID.ToByteArray()),
            AuthorId = article.AuthorId,

            Title = article.Title,
            CoverImage = article.Cover,
            Content = article.Content,
            Description = article.ContentPreviews,
            Tags = article.Tags,
            LastUpdatedTime = article.LastUpdated,
            CreatedTime = article.CreatedAt,

            IsPublished = article.IsPublished,
            IsLiked = article.LikedBy.Contains(request.sub ?? request.ArticleId),
            IsSaved = article.SavedBy.Contains(request.sub ?? request.ArticleId),
            LikeCount = article.LikedCount
        });
    }
}

