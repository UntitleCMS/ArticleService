using Infrastructure.Data;
using Domain.Entity;
using BlogService.Services.PostServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace BlogService.Controllers.PostControllers
{

    //[Route("[controller]")]
    //[ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        //[HttpGet("/posts")]
        //[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public IActionResult GetPosts()
        {
            var posts = _postService.GetPosts();
            return Ok(posts);
        }

        //[HttpGet("{postId}")]
        public IActionResult GetPost([FromRoute()] Guid postId)
        {
            var post = _postService.GetPost(postId);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        //[HttpPost]
        //[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public IActionResult AddPost([FromBody] Post post)
        {
            _postService.AddPost(post);
            return NoContent();
        }

        //[HttpPut]
        //[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public IActionResult UpdatePost([FromBody] Post post)
        {
            _postService.UpdatePost(post);
            return NoContent();
        }

        //[HttpDelete("{postId}")]
        //[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public IActionResult DeletePost([FromRoute()] Guid postId)
        {
            var post = _postService.GetPost(postId);
            if (post == null)
                return NotFound();
            _postService.Delete(post);
            return NoContent();
        }
    }
}
