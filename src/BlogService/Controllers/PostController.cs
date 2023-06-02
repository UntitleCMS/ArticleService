using Application.Common.Dtos;
using Application.Services.PostService;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var p = postService.AllPost();
            return Ok(p.AsNoTracking());
        }

        [HttpGet]
        [Route("{postId}")]
        public IActionResult Post(Guid postId)
        {
            return Ok(postService.Find(postId));
        }

        [HttpPost]
        public IActionResult AddPost(PostRequestAddDto newPost)
        {
            return Ok(postService.Add(newPost));
        }

        [HttpPut] 
        [Route("{postId}")]
        public IActionResult Update([FromRoute]Guid postId,[FromBody]Post post)
        {
            post.ID = postId;
            postService.Update(post);
            return Ok(post);
        }
    }
}
