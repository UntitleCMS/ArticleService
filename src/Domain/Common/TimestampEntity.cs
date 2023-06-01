using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
    public class TimestampEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdated { get; }

    }
}
