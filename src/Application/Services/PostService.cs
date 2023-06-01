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

        public Post Update(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
            return post;
        }

        public void Delete(Guid id)
        {
            var p =_context.Posts.First(p => p.PostID == id);
            _context.Posts.Remove(p);
            _context.SaveChanges();
        }

    }
}
