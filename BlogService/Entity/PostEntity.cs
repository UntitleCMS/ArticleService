using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BlogService.Entity
{
    public class PostEntity
    {
        [Key]
        public Guid PostID { get; set; }

        [Required]
        public string OwnerID { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdated { get; set; } 

        [Required]
        public string PostTitle { get; set; } = string.Empty;

        [AllowNull]
        public string? Thumbnail { get; set; }

        [Required]
        public string Contest { get; set; } = string.Empty;
    }
}
