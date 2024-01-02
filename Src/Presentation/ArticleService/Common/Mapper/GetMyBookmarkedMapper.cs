using Application.Features.Article.Query.GetArticels;
using Application.Features.Bookmark.Query;
using ArticleService.Dtos;

namespace ArticleService.Common.Mapper;

public static class GetMyBookmarkedMapper
{
    public static GetMyBookmarksQuery GetMyBookmarksQuery(this ArticlesQueryDto dto) => new()
    {
        Sub = dto.Sub,
        Filter = dto.Filter,
        Privot = dto.Privot,
        Take = dto.Take,
        Tags = dto.Tags,
        IsBookmarked = true
    };
}

