using Application.Common.Extentions;
using Application.Common.Mediator;
using Application.Posts.Command;
using Application.Posts.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

    // GET /posts
    [HttpGet]
    public async Task<object> GetAllPost(
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

    // POST /posts
    [HttpPost]
    public async Task<IActionResult> AddNewPost(
        [FromBody] AddPostCommand newPost,
        [FromQuery] string sub)
    {
        newPost.Sub = sub;
        var a = await _mediator.Send(newPost);
        return Ok(a);
    }

    // GET /posts/:id
    [HttpGet("{id}")]
    public async Task<Object> GetPost(
        [RegularExpression(@"^[A-Za-z0-9_-]{22}$", ErrorMessage = "Invalid ID")]
        string id)
    { 
        return await _mediator.Send(new GetPostDetailQuery() { Id = id});
    }

    // DELETE /posts/:id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost([FromRoute]string id, string sub)
    {
        var a = await _mediator.Send(new DeletePostCommand()
        {
            PostId = id.ToGuid(),
            UserId = sub.ToGuid()
        });
        return Ok(a); 
    }

    // PUT /posts/:id
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(
        [FromRoute] string id,
        [FromQuery] string sub,
        [FromBody] UpdatePostCommand newPost)
    {
        newPost.PostId = id.ToGuid();
        try
        {
            newPost.AuthorId = sub.ToGuid();
        }catch (Exception)
        {
            newPost.AuthorId = new Guid(sub);
        }

        var a = await _mediator.Send(newPost);
        return Ok(a);
    }
}
