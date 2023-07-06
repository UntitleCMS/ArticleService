using Application.Common.Extentions;
using Application.Posts.Command;
using Application.Posts.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;

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
        [FromQuery] int? take,
        [FromQuery] string? before,
        [FromQuery] string? after)
    {
        bool isHaveReferancePostId = !(before is not null && after is not null);
        bool ishaveBefore = !before.IsNullOrEmpty();

        if ( !isHaveReferancePostId )
            return BadRequest("Too many reference");

        GetAllPostsQuery req = !ishaveBefore && after.IsNullOrEmpty()
            ? new()
            : new( ishaveBefore ? "BEFORE" : "AFTER", ishaveBefore ? before! : after!, take);

        return await _mediator.Send(req);
    }

    [HttpPost]
    public async Task<string> AddNewPost(
        [FromBody] AddPostCommand newPost,
        [FromQuery] string sub)
    {
        newPost.Sub = sub;
        var a = await _mediator.Send(newPost);
        return a;
    }

    [HttpGet("{id}")]
    public async Task<Object> GetPost(string id)
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
