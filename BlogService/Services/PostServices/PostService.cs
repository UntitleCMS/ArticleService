using BlogService.Data;
using BlogService.Entity;
using BlogService.Services.PostServices.Interfaces;

namespace BlogService.Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _DbContext;

        public PostService(AppDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public void AddPost(Post post)
        {
            _DbContext.Posts.Add(post);
            DoSave();
        }

        public void Delete(Guid postId)
        {
            var post = GetPost(postId);
            Delete(post!);
        }

        public void Delete(Post post)
        {
            _DbContext.Remove(post);
            DoSave();
        }

        public Post? GetPost(Guid postId)
        {
            var post = _DbContext.Posts.Find(postId);
            return post;
        }

        public IQueryable<Post> GetPosts()
        {
            return _DbContext.Posts.AsQueryable();
        }

        public void UpdatePost(Post post)
        {
            _DbContext.Update(post);
            DoSave();
        }

        private void DoSave()
        {
            _DbContext.SaveChanges();
        }
    }
}
