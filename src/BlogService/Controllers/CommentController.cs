using Application.Common.Interfaces;
using Application.Services.CommentService;
using Application.Services.CommentService.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogService.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentService commentService;

        public CommentController(CommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpPut] 
        [Route("{postId}/comment")]
        public IActionResult AddComment([FromRoute]Guid postId, [FromBody] CommentRequestAdd comment)
        {
            return Ok(commentService.AddToPost(postId, comment));
        }
        
        [HttpDelete] 
        [Route("{postId}/comment")]
        public IActionResult RemoveComment([FromRoute]Guid postId ,[FromBody]Guid CommentId)
        {
            commentService.RemoveCommentFromPost(postId, CommentId);
            return NoContent();
        }

    }
}
