using System.ComponentModel.DataAnnotations.Schema;

namespace BlogService.Entity
{
    public class TimestampEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdated { get; set; }

    }
}
