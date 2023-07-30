
using Infrastructure.Common.Models;

namespace Infrastructure.Collections;

public class CommentCollection : BaseTimestampCollection<Guid>
{
    // identity
    public Guid AuthorId { get; set; }

    // reference
    public Guid PostId { get; set; }
    public Guid ReplyTo { get; set; }

    // content
    public string Content { get; set; } = string.Empty;
}
