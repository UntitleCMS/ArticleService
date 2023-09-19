using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Persistence.Collections;

public class PostCollection
{
    [BsonId]
    public virtual Guid ID { get; set; }

    public virtual string AuthorId { get; set; } = string.Empty;

    public virtual string Title { get; set; } = string.Empty;
    public virtual string Content { get; set; } = string.Empty;
    public virtual string ContentPreviews { get; set; } = string.Empty;
    public virtual string Cover { get; set; } = string.Empty;

    // post metadata
    public virtual IList<string> Tags { get; set; } = Array.Empty<string>();
    public virtual bool IsPublished { get; set; } = false;
    public virtual DateTime Timestamp { get; set; } = DateTime.Now;
    public virtual DateTime LastUpdate { get; set; } = DateTime.Now;

    // state
    public virtual IList<string> LikedBy { get; set; } = Array.Empty<string>();
    public virtual IList<string> SavedBy { get; set; } = Array.Empty<string>();
}
