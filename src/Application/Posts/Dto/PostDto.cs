using Application.Common.Extentions;
using Domain.Entity;

namespace Application.Posts.Dto;

public class PostDto
{
    public string Id { get; set; } = string.Empty;
    public string AuthorId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Content { get; set; }
    public bool IsPublished { get; set; } 
    public string? CoverImage { get;  set; }
    public DateTime LastUpdatedTime { get;  set; }
    public DateTime CreatedTime { get;  set; }
    public ICollection<string>? Tags { get;  set; }

    public PostDto()
    {
        
    }
    public PostDto(Post p)
    {
        Id = p.ID.ToBase64Url();
        AuthorId = p.Author.ToBase64Url();
        Title = p.Title;
        Description = p.ContentPreviews;
        Content = p.Content;
        IsPublished = p.IsPublished ?? false;
        CoverImage = p.Cover;
        LastUpdatedTime = p.LastUpdated;
        CreatedTime = p.CreatedAt;
        Tags = p.Tags;
    }
}
