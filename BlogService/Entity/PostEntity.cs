using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogService.Entity
{
    public class PostEntity
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string PostTitle { get; set; } = string.Empty;
    }
}
