using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Common.models;
using Application.Common.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Bookmark.Query;

using ResponseType = Pageable<GetMyBookmarksQueryDto>;
public class GetMyBookmarksHandler : RequestPipeHandelerBase<GetMyBookmarksQuery, ResponseType>
{
    private IArticlesPageableRepository _repo;

    public GetMyBookmarksHandler(IArticlesPageableRepository repo)
    {
        _repo = repo;
    }

    protected override string DefaultErrorMessage => "Can not get bookmarks";

    protected override async Task<IResponseWrapper<ResponseType>> Execute(GetMyBookmarksQuery request)
    {
        Console.WriteLine("**********************");
        ArticleFilter filler = new()
        {
            IsBookmarked = request.IsBookmarked,
            Of = request.Filter,
            Tags = request.Tags,
            Take = request.Take
        };

        if (request.Privot?.StartsWith("<") ?? false)
        {
            filler.Before = request.Privot[1..];
        }
        else if (request.Privot?.StartsWith(">") ?? false)
        {
            filler.After = request.Privot[1..];
        }

        var res = await _repo.Find(filler, request.Sub);

        var col = res.Collections.Select(article => new GetMyBookmarksQueryDto()
        {
            Id = Base64UrlEncoder.Encode(article.ID.ToByteArray()),
            AuthorId = article.AuthorId,

            Title = article.Title,
            CoverImage = article.Cover,
            Description = article.ContentPreviews,
            Tags = article.Tags,
            LastUpdatedTime = article.LastUpdated,
            CreatedTime = article.CreatedAt,

            IsPublished = article.IsPublished,
            IsLiked = article.LikedBy.Contains(request.Sub),
            IsSaved = article.SavedBy.Contains(request.Sub),
            LikeCount = article.LikedCount
        });

        return Ok(new()
        {
            Collections = col,
            CountAll = res.CountAll,
            HasNext = res.HasNext,
            HasPrevious = res.HasPrevious,
            Pivot = res.Pivot
        });
    }
}
