using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Common.models;
using Application.Common.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Article.Query.GetArticels;

using ResponseType = Pageable<GetArticelsQueryDto>;

public class GetArticelsQueryHandeler : RequestPipeHandelerBase<GetArticelsQuery, ResponseType>
{
    private readonly IPostRepository _repo;
    private readonly IArticlesPageableRepository _article;

    public GetArticelsQueryHandeler(IPostRepository repo, IArticlesPageableRepository article)
    {
        _repo = repo;
        _article = article;
    }

    protected override string DefaultErrorMessage => "Can not query articles";
    private int DefualSize => 20;

    [Obsolete("This method is lagacy")]
    private async Task<IResponseWrapper<ResponseType>> Execute_Backup(GetArticelsQuery request)
    {
        var size = request.Take == 0 ? DefualSize : request.Take;
        var res = request.Privot is null
            ? await _repo.Find(Take: size, Of: request.Filter, Tags: request.Tags, Sub: request.Sub)
            : request.Privot.First() == '<'
            ? await _repo.Find(Take: size, Befor: request.Privot[1..], Of: request.Filter, Tags: request.Tags, Sub: request.Sub)
            : await _repo.Find(Take: size, After: request.Privot[1..], Of: request.Filter, Tags: request.Tags, Sub: request.Sub);

        // maping model
        var col = res.Collections.Select(article => new GetArticelsQueryDto()
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

    protected override async Task<IResponseWrapper<ResponseType>> Execute(GetArticelsQuery request)
    {
        var size = request.Take == 0 ? DefualSize : request.Take;

        ArticleFilter filler = new()
        {
            IsBookmarked = request.IsBookmarked,
            IsPublish = request.IsPublish,
            Of = request.Filter,
            Tags = request.Tags,
            Take = request.Take,
            OnlyFollowing = request.OnlyFollowing
        };

        if (request.Privot?.StartsWith("<") ?? false)
        {
            filler.Before = request.Privot[1..];
        }
        else if (request.Privot?.StartsWith(">") ?? false)
        {
            filler.After = request.Privot[1..];
        }

        filler.Before?.Replace("TermsAndPolicy", "AAAAAAAAAECAAAAAAAAAAA").Replace("SupportedProgrammingLanguages","EREREREREUGREREREREREQ");
        filler.After?.Replace("TermsAndPolicy", "AAAAAAAAAECAAAAAAAAAAA").Replace("SupportedProgrammingLanguages","EREREREREUGREREREREREQ");

        var res = await _article.Find(filler, request.Sub);


        // maping model
        var col = res.Collections.Select(article => new GetArticelsQueryDto()
        {
            Id = Base64UrlEncoder.Encode(article.ID.ToByteArray()) == "AAAAAAAAAECAAAAAAAAAAA"
                ? "TermsAndPolicy"
                : Base64UrlEncoder.Encode(article.ID.ToByteArray()) == "EREREREREUGREREREREREQ"
                ? "SupportedProgrammingLanguages"
                : Base64UrlEncoder.Encode(article.ID.ToByteArray()),
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
