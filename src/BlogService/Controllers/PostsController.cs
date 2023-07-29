using Application.Common.Extentions;
using Application.Common.Mediator;
using Application.Posts.Command;
using Application.Posts.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace BlogService.Controllers;

[Route("[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<Object> GetAllPost(
        [FromQuery, Range(1,100)] int? take,
        [RegularExpression(@"^[A-Za-z0-9_-]{22}$", ErrorMessage = "Invalid Id.")]
        [FromQuery] string? before,
        [RegularExpression(@"^[A-Za-z0-9_-]{22}$", ErrorMessage = "Invalid Id.")]
        [FromQuery] string? after)
    {
        bool isHaveReferancePostId = !(before is not null && after is not null);
        bool ishaveBefore = !before.IsNullOrEmpty();

        if (!isHaveReferancePostId)
            return BadRequest(new Response<object>()
            {
                IsSuccess = false,
                Message = "Too manny referecne"
            });

        GetAllPostsQuery req = !ishaveBefore && after.IsNullOrEmpty()
            ? new()
            : new()
            {
                RefPostId = ishaveBefore ? before!.ToGuid() : after!.ToGuid(),
                Take = take ?? 20
            };

        req.Take *= ishaveBefore ? 1 : after.IsNullOrEmpty()? 1 : -1;

        return await _mediator.Send(req);
    }

    [HttpPost]
    public async Task<IActionResult> AddNewPost(
        [FromBody] AddPostCommand newPost,
        [FromQuery] string sub)
    {
        newPost.Sub = sub;
        var a = await _mediator.Send(newPost);
        return Ok(a);
    }

    [HttpGet("{id}")]
    public async Task<Object> GetPost(
        [RegularExpression(@"^[A-Za-z0-9_-]{22}$", ErrorMessage = "Invalid ID")]
        string id)
    { 
        return await _mediator.Send(new GetPostQuery(id));
    }

    [HttpDelete("{id}")]
    public async Task<string> DeletePost([FromRoute]string id, string sub)
    {
        var a = await _mediator.Send(new DeletePostCommand()
        {
            PostId = id.ToGuid(),
            UserId = sub
        });
        return a; 
    }
}
