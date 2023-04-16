using BlogService.Entity;

namespace BlogService.Services.PostServices.Interfaces
{
    public interface IPostService
    {
        Post GetPost(Guid postId);
        IQueryable<Post> GetPosts();
    }
}
