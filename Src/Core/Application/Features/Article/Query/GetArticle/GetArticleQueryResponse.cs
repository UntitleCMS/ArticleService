using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Article.Query.GetArticle;

public record GetArticleQueryResponse
{
    // identity
    public virtual string Id { get; set; } = string.Empty;
    public virtual string AuthorId { get; set; } = string.Empty;

    // contents
    public virtual string Title { get; set; } = string.Empty;
    public virtual string CoverImage { get; set; } = string.Empty;
    public virtual string Description { get; set; } = string.Empty;
    public virtual string Content { get; set; } = string.Empty;

    // metadata
    public virtual bool IsPublished { get; set; }
    public virtual IList<string> Tags { get; set; } = Array.Empty<string>();
    public virtual DateTime CreatedTime { get; set; }
    public virtual DateTime LastUpdatedTime { get; set; }

    // state
    public virtual int LikeCount { get; set; }
    public virtual bool? IsLiked { get; set; }
    public virtual bool? IsSaved { get; set; }
}

