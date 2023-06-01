using Application.Services;
using Domain.Entity;
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

        [HttpPost]
        public IActionResult AddPost(Post newPost)
        {
            postService.Add(newPost);
            return Ok(newPost);
        }

        [HttpPut] 
        [Route("{postId}")]
        public IActionResult Update([FromRoute]Guid postId,[FromBody]Post post)
        {
            post.PostID = postId;
            postService.Update(post);
            return Ok(post);
        }
    }
}
