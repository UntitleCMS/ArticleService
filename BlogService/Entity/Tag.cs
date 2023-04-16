using System.ComponentModel.DataAnnotations;

namespace BlogService.Entity
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [StringLength(20)]
        [Required]
        public string TagName { get; set; } = string.Empty;

        [StringLength(8)]
        [Required]
        public string TagColour { get; set; } = string.Empty;

        public ICollection<Post>? Posts { get; set; }
    }
}
