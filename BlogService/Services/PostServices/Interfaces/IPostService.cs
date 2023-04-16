using BlogService.Entity;

namespace BlogService.Services.PostServices.Interfaces
{
    public interface IPostService
    {
        Post? GetPost(Guid postId);
        IQueryable<Post> GetPosts();
        void AddPost(Post post);
        void UpdatePost(Post post);
        void Delete(Guid postId);
        void Delete(Post post);
    }
}
