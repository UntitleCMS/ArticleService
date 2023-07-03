using Application.Common.Extentions;
using Application.Services.PostService;
using Application.Services.PostService.Dtos;
using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace BlogService.Controllers
{
    [Route("posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService postService;

        public PostController(PostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AllPosts(int? page, int? size)
        {
            if (page is not null && size is not null)
            {
                return Ok(postService.GetPage(page.Value, size.Value));
            }
            var p = postService.AllPost()
                .Select(a => new
                {
                    ID = a.ID.ToBase64Url(),
                    Comments = a.Comments,
                    Contest = a.Contest,
                    CreatedAt = a.CreatedAt,
                    IsPublished = a.IsPublished,
                    LastUpdated = a.LastUpdated,
                    OwnerID = a.OwnerID,
                    PostTitle = a.PostTitle,
                    Tags = a.Tags,
                    Thumbnail = a.Thumbnail
                } );
            return Ok(p.AsNoTracking());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{postId}")]
        public IActionResult Post(Guid postId)
        {
            return Ok(postService.Find(postId));
        }

        [HttpPost]
        public IActionResult AddPost(
            [FromBody]  PostRequestAddDto newPost,
            [FromQuery] string? ownerID)
        {
            //string? ownerID = null;
            if (ownerID is null)
                return BadRequest("Invalid Token");

            return Ok(postService.Add( ownerID, newPost));
        }

        [HttpPut] 
        [Route("{postId}")]
        public IActionResult Update([FromRoute]Guid postId,[FromBody]PostRequestAmendDto post)
        {
            postService.Update(postId, post);
            return Ok(post);
        }

        [HttpDelete]
        [Route("{postId}")]
        public IActionResult Delete(Guid postId)
        {
            postService.Delete(postId);
            return NoContent();
        }

    }
}
