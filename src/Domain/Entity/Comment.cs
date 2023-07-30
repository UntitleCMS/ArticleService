using Domain.Common;

namespace Domain.Entity;

internal class Comment : TimestampEntity<Guid>
{
    public Guid PostId { get; set; }
    public Guid? ReplyTo { get; set; }  // null for top-level comments

    // content
    public string Content { get; set; } = string.Empty;
}
