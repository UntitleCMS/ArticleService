using Application.Common.Extentions;
using Domain.Entity;

namespace Application.Posts.Dto;

public class PostDto
{
    public string Id { get; set; }
    public string OwnerId { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string Content { get; set; }
    public bool IsPublish { get; set; }
    public string? Cover { get;  set; }
    public DateTime LastUpdate { get;  set; }
    public DateTime CreateAt { get;  set; }
    public ICollection<string>? Tags { get;  set; }

    public PostDto()
    {
        
    }
    public PostDto(Post p)
    {
        Id = p.ID.ToBase64Url();
        OwnerId = p.Author.ToBase64Url();
        Title = p.Title;
        SubTitle = p.ContentPreviews;
        Content = p.Content;
        IsPublish = p.IsPublished ?? false;
        Cover = p.Cover;
        LastUpdate = p.LastUpdated;
        CreateAt = p.CreatedAt;
        Tags = p.Tags;
    }
}
