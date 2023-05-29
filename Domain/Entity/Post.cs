using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entity
{
    public class Post : TimestampEntity
    {
        [Key]
        public Guid PostID { get; set; }

        [Required]
        public string OwnerID { get; set; } = string.Empty;

        [Required]
        public string PostTitle { get; set; } = string.Empty;

        [AllowNull]
        public string? Thumbnail { get; set; }

        [Required]
        public string Contest { get; set; } = string.Empty;

        public bool IsPublished { get; set; } = false;

        public ICollection<Tag>? Tags { get; set; }

        public ICollection<Comment>? Comments { get; set; }
    }
}
