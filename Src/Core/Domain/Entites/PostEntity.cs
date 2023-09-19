namespace Domain.Entites;

public class PostEntity : BaseAuditableEntity<Guid,string>
{
    // post content
    public virtual string Title { get; set; } = string.Empty;
    public virtual string Content { get; set; } = string.Empty;
    public virtual string ContentPreviews { get; set; } = string.Empty;
    public virtual string Cover { get; set; } = string.Empty;

    // post metadata
    public virtual IList<string> Tags { get; set; } = Array.Empty<string>();
    public virtual bool IsPublished { get; set; } = false;

    // state
    public virtual int LikedCount { get; set; }
    public virtual IList<string> LikedBy { get; set; } = Array.Empty<string>();
    public virtual IList<string> SavedBy { get; set; } = Array.Empty<string>();
}
