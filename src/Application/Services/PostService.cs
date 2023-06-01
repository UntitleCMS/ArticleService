using Application.Common.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PostService
    {
        private readonly IAppDbContext _context;

        public PostService(IAppDbContext context)
        {
            _context = context;
        }

        public Post? Find(Guid id)
        {
            return _context.Posts.FirstOrDefault( p => p.PostID == id );
        } 

        public IEnumerable<Post> AllPost()
        {
            return _context.Posts.ToList();
        }

        public Post Add(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post;
        }

    }
}
