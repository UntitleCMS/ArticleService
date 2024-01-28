using Application.Features.Article.Query.GetArticels;
using ArticleService.Dtos;

namespace ArticleService.Common.Mapper;

public static class ArticlesQueryMapper
{
    public static GetArticelsQuery GetQuery(this ArticlesQueryDto dto) => new()
    {
        Sub = dto.Sub,
        Filter = dto.Filter,
        Privot = dto.Privot,
        Take = dto.Take,
        Tags = dto.Tags,
        OnlyFollowing = dto.OnlyFollowing
    };
}
