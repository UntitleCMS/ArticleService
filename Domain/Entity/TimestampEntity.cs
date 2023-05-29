using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class TimestampEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdated { get; }

    }
}
