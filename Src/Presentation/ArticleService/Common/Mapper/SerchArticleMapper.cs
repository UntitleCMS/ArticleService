using Application.Features.Article.Query.GetArticels;
using Application.Features.Article.Query.SerchArticle;
using Application.Features.Bookmark.Query;
using ArticleService.Dtos;

namespace ArticleService.Common.Mapper;

public static class SerchArticleMapper
{
    public static SerchArticelsQuery GetSerchArticleQuery(this ArticlesQueryDto dto) => new()
    {
        Sub = dto.Sub,
        Filter = dto.Filter,
        Privot = dto.Privot,
        Take = dto.Take,
        Tags = dto.Tags,
        IsBookmarked = dto.IsBookmared,
        SerchText = dto.SerchText,
    };
}

