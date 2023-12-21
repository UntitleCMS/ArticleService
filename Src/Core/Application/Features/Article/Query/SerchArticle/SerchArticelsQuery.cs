using Application.Common.Mediator;
using Application.Common.models;
using Application.Features.Article.Query.GetArticels;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Article.Query.SerchArticle;

using ResponseType = Pageable<SerchArticelsQueryDto>;
public class SerchArticelsQuery : IRequestWraped<ResponseType>
{
    public string? Sub { get; set; }

    public int Take { set; get; } = 20;

    [RegularExpression("""^[<>][A-Za-z0-9_\-\+\/]{22,24}$""", ErrorMessage = "Invalid Privot")]
    public string? Privot { set; get; }

    [RegularExpression("""^[A-Za-z0-9_\-\+\/]{22,24}$""", ErrorMessage = "For base64 id")]
    public string? Filter { get; set; }
    public string? SerchText { get; set; }
    public string[]? Tags { get; set; }
    public bool IsBookmarked { get; set; } = false;
}
