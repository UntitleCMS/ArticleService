using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Dtos
{
    public class PostRequestAddDto
    {
        [Required]
        public string PostTitle { get; set; } = string.Empty;
        [Required]
        public string Thumbnail { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        public bool IsPublished { get; set; } = false;
        public List<int> Tags { get; set; } = new();
    }
}
