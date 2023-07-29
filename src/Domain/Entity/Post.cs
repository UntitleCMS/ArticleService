using Domain.Common;

namespace Domain.Entity;

public class Post :  TimestampEntity<Guid>
{
    // identity
    public Guid Author { get; set; }

    // post content
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string ContentPreviews { get; set; } = string.Empty;
    public string Cover { get; set; } = string.Empty;

    // post metadata
    public IList<string>? Tags { get; set; }
    public IList<Tag>? TagDetails { get; set; }
    public IList<string>? Keywords { get; set; }

    // state
    public bool? IsPublished { get; set; }
    public bool? IsLiked { get; set; }
    public bool? IsSave { get; set; }
}
