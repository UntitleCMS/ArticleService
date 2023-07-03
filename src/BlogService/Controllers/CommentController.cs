using Application.Common.Interfaces;
using Application.Services.CommentService;
using Application.Services.CommentService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace BlogService.Controllers;

[Route("api/post")]
[ApiController]
[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
public class CommentController : ControllerBase
{
    private readonly CommentService commentService;

    public CommentController(CommentService commentService)
    {
        this.commentService = commentService;
    }

    [HttpPost] 
    [Route("{postId}/comment")]
    public IActionResult AddComment([FromRoute]Guid postId, [FromBody] CommentRequestAdd comment)
    {
        var ownerID = User.GetClaim(Claims.Subject);
        if (ownerID is null)
            return BadRequest("Invalid Token");
        return Ok(commentService.AddToPost(ownerID, postId, comment));
    }
    
    [HttpDelete] 
    [Route("{postId}/comment")]
    public IActionResult RemoveComment([FromRoute]Guid postId ,[FromBody]Guid CommentId)
    {
        commentService.RemoveCommentFromPost(postId, CommentId);
        return NoContent();
    }

}
