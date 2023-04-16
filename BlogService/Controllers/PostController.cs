using BlogService.Data;
using BlogService.Entity;
using BlogService.Services.PostServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogService.Controllers
{

    [Route("blog-service/api/v1/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    { 
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("/blog-service/api/v1/posts")]
        public IActionResult GetPosts()
        {
            var posts = _postService.GetPosts();
            return Ok(posts);
        }

        [HttpGet("{postId}")]
        public IActionResult GetPost( [FromRoute()] Guid postId)
        {
            var post = _postService.GetPost(postId);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        [HttpPost]
        public IActionResult AddPost([FromBody] Post post)
        {
            _postService.AddPost(post);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdatePost([FromBody] Post post)
        {
            _postService.UpdatePost(post);
            return NoContent();
        }

        [HttpDelete("{postId}")]
        public IActionResult DeletePost( [FromRoute()] Guid postId)
        {
            var post = _postService.GetPost(postId);
            if (post == null)
                return NotFound();
            _postService.Delete(post);
            return NoContent();
        }
    }
}
