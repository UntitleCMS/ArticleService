using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity;

internal class CommentModel : TimestampEntity<Guid>
{
    public Guid PostId { get; set; }
    public Guid? ReplyTo { get; set; }  // null for top-level comments

    // content
    public string Content { get; set; } = string.Empty;
}
