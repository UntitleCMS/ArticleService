using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entity
{
    public class Comment : TimestampEntity<Guid>
    {
        //[Key]
        //public Guid ID { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [ForeignKey("Posts")]
        public Guid PostId { get; set; }

        Post? Post { get; set; }
    }
}
