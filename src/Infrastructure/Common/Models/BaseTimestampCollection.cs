
namespace Infrastructure.Common.Models;

public class BaseTimestampCollection<TID>
    : BaseCollection<TID>
    where TID : IEquatable<TID>
{
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdate { get; set; }
}
