using Application.Common.Extentions;
using Domain.Entity;

namespace Application.Posts.Dto;

public class PostDto
{
    public string Id { get; set; }
    public string OwnerId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPublish { get; set; }
    public string? Cover { get; private set; }
    public DateTime LastUpdate { get; private set; }
    public DateTime CreateAt { get; private set; }
    public ICollection<string>? Tags { get; private set; }
    public ICollection<Comment>? Comments { get; private set; }

    public PostDto(Post p)
    {
        Id = p.ID.ToBase64Url();
        OwnerId = p.OwnerID.ToBase64Url();
        Title = p.PostTitle;
        Content = p.Contest;
        IsPublish = p.IsPublished;
        Cover = p.Thumbnail;
        LastUpdate = p.LastUpdated;
        CreateAt = p.CreatedAt;
        Tags = p.Tags;
        Comments = p.Comments;
    }
}
