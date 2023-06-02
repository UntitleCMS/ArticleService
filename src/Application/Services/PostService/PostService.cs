using Application.Common.Interfaces;
using Application.Services.PostService.Dtos;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PostService
{
    public sealed class PostService
    {
        private readonly IAppDbContext _context;

        public PostService(IAppDbContext context)
        {
            _context = context;
        }

        public Post? Find(Guid id)
        {
            return _context.Posts
                .Include(p=>p.Tags)
                .Include(p=>p.Comments)
                .FirstOrDefault(p => p.ID == id);
        }

        public IQueryable<Post> AllPost()
        {
            return _context.Posts
                .OrderBy(p => p.CreatedAt)
                .Include(p => p.Tags);
        }

        public Post Add(PostRequestAddDto post)
        {

            var newPost = new Post()
            {
                PostTitle = post.PostTitle,
                Thumbnail = post.Thumbnail,
                Contest = post.Content,
                IsPublished = post.IsPublished,
                Tags = _context.Tags
                    .Where(t=>post.Tags.Contains(t.ID))
                    .ToList()
            };
            _context.Posts.Add(newPost);
            _context.SaveChanges();
            _context.Entry(newPost).Collection(p => p.Tags!).Load();
            return newPost;
        }

        public Post Update(Guid pid, PostRequestAmendDto post)
        {
            var p = _context.Posts.Single(i => i.ID == pid);

            p.PostTitle = post.PostTitle;
            p.Thumbnail = post.Thumbnail;
            p.IsPublished = post.IsPublished;
            p.Contest = post.Content;
            p.Tags = _context.Tags.Where(t=>post.Tags.Contains(t.ID)).ToList();

            _context.Posts.Update(p);
            _context.SaveChanges();
            return p;
        }

        public void Delete(Guid id)
        {
            var p = _context.Posts.First(p => p.ID == id);
            _context.Posts.Remove(p);
            _context.SaveChanges();
        }

    }
}
