using Application.Common.Interfaces;
using Application.Services.CommentService;
using Application.Services.CommentService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlogService.Controllers;

[Route("posts")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly CommentService commentService;

    public CommentController(CommentService commentService)
    {
        this.commentService = commentService;
    }

    [HttpPost] 
    [Route("{postId}/comments")]
    public IActionResult AddComment(
        [FromRoute]Guid postId,
        [FromBody] CommentRequestAdd comment,
        [FromQuery]string? ownerID)
    {
        //string? ownerID = null;
        if (ownerID is null)
            return BadRequest("Invalid Token");
        return Ok(commentService.AddToPost(ownerID, postId, comment));
    }
    
    [HttpDelete] 
    [Route("{postId}/comments")]
    public IActionResult RemoveComment([FromRoute]Guid postId ,[FromBody]Guid CommentId)
    {
        commentService.RemoveCommentFromPost(postId, CommentId);
        return NoContent();
    }

}
