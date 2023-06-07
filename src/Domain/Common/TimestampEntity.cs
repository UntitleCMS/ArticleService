using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
    public abstract class TimestampEntity<ID>
        : BaseEntity<ID>
        where ID : IEquatable<ID> 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = default;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdated { get; set; } = default;

    }
}
