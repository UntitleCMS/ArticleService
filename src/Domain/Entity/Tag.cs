using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Tag : BaseEntity<string>
    {
        //[Key]
        //public int ID { get; set; }

        [StringLength(20)]
        [Required]
        public override string ID { get; set; } = string.Empty;

        [StringLength(8)]
        [Required]
        public string TagColour { get; set; } = string.Empty;

        public ICollection<Post>? Posts { get; set; }
    }
}
