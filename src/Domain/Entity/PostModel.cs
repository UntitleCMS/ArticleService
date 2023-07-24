using Domain.Common;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity;

public class PostModel :  TimestampEntity<Guid>
{
    // post content
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string ContentPreviews { get; set; } = string.Empty;
    public string Cover { get; set; } = string.Empty;

    // post metadata
    public bool IsPublished { get; set; }

    // keywords and tags
    public IList<string>? Keyword { get; set; }
    public IList<string>? Tags { get; set; }

    // statistics
    public IList<Guid>? LikedByUserIds { get; set; }

    // community
    public IList<string>? Comments { get; set; }
}
