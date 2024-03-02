using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Common.models;
using Application.Common.Repositories;
using Application.Features.Article.Query.GetArticels;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Article.Query.SerchArticle;

using ResponseType = Pageable<SerchArticelsQueryDto>;

public class SerchArticelsQueryHandeler : RequestPipeHandelerBase<SerchArticelsQuery, ResponseType>
{
    private readonly IArticlesPageableRepository _article;

    public SerchArticelsQueryHandeler(IArticlesPageableRepository article)
    {
        _article = article;
    }

    protected override string DefaultErrorMessage => "Can not query articles";
    private int DefualSize => 20;
    protected override async Task<IResponseWrapper<ResponseType>> Execute(SerchArticelsQuery request)
    {
        var size = request.Take == 0 ? DefualSize : request.Take;

        ArticleFilter filler = new()
        {
            IsBookmarked = request.IsBookmarked,
            Of = request.Filter,
            Tags = request.Tags,
            Take = request.Take,
            SerchText = request.SerchText
        };

        if (request.Privot?.StartsWith("<") ?? false)
        {
            filler.Before = request.Privot[1..];
        }
        else if (request.Privot?.StartsWith(">") ?? false)
        {
            filler.After = request.Privot[1..];
        }

        var res = await _article.Find(filler, request.Sub);


        // maping model
        var col = res.Collections.Select(article => new SerchArticelsQueryDto()
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
