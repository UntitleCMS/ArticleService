using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Domain.Common;

namespace Domain.Entity
{
    public class Post : TimestampEntity<Guid>
    {
        [Required]
        public Guid OwnerID { get; set; }

        [Required]
        public string PostTitle { get; set; } = string.Empty;
        [Required]
        public string PostSubTitle { get; set; } = string.Empty;

        [AllowNull]
        public string? Thumbnail { get; set; }

        [Required]
        public string Contest { get; set; } = string.Empty;

        public bool IsPublished { get; set; } = false;

        public ICollection<string>? Tags { get; set; }

        public ICollection<Comment>? Comments { get; set; }
    }
}
