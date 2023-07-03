using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PostService.Dtos
{
    public class PostPagableResponDto
    {
        [NotNull]
        public int PageCount { get; set; }

        [NotNull] 
        public int PageSize { get; set; }

        [NotNull]
        public int CurentPage { get; set; }

        public List<Post> Data { get; set; } = new();

        public bool HasNext { get; set; }
        public Post? Next { get; set; } 
    }
}
