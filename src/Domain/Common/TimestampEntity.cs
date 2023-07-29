
namespace Domain.Common
{
    public abstract class TimestampEntity<ID> : BaseEntity<ID>
        where ID : IEquatable<ID> 
    {
        public DateTime CreatedAt { get; set; } = default;
        public DateTime LastUpdated { get; set; } = default;
    }
}
