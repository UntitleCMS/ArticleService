
using Domain.Entity;
using Infrastructure.Collections;

namespace Infrastructure.Common.Mappers;

public static class PostMapper
{
    public static Post ToPost(this PostCollection post) => new()
    {
        ID = post.Id,
        Author = post.AuthorId,

        Title = post.Title,
        ContentPreviews = post.ContentPreview,
        Content = post.Content,
        Cover = post.Cover,

        CreatedAt = post.CreatedAt,
        LastUpdated = post.LastUpdate,

        IsPublished = post.IsPublished,

        Tags = post.Tags,

        Keywords = post.Keywords,
    };

    public static PostCollection ToPostCollection(this Post post) => new()
    {
        Id = post.ID,
        AuthorId = post.Author,

        Title = post.Title,
        Content = post.Content,
        ContentPreview = post.ContentPreviews,
        Cover = post.Cover,

        CreatedAt = post.CreatedAt,
        LastUpdate = post.LastUpdated,

        IsPublished = post.IsPublished ?? false,

        Tags = post.Tags,
        Keywords = post.Keywords,
    };
}
