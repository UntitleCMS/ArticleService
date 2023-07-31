
using Infrastructure.Common.Models;

namespace Infrastructure.Collections;

public class PostCollection : BaseTimestampCollection<Guid>
{
    // identity
    public Guid AuthorId { get; set; }

    // post content
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string ContentPreview { get; set; } = string.Empty;
    public string Cover { get; set; } = string.Empty;

    // post metadata
    public bool IsPublished { get; set; }
    public IList<string>? Keywords { get; set; }
    public IList<string>? Tags { get; set; }

    // statistics
    public IList<Guid>? LikedByUserIds { get; set; }

    // community
    public IList<Guid>? CommentIds { get; set; }
}
