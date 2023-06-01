using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace BlogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService postService;

        public PostController(PostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        [Route("~/api/posts")]
        public IActionResult AllPosts()
        {
            return Ok(postService.AllPost());
        }

        [HttpGet]
        [Route("{postId}")]
        public IActionResult Post(Guid postId)
        {
            return Ok(postService.Find(postId));
        }
    }
}
