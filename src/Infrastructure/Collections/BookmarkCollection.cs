
using Infrastructure.Common.Models;

namespace Infrastructure.Collections;

public class BookmarkCollection : BaseCollection<Guid>
{
    public IList<Guid>? PostId { get; set; }
}
