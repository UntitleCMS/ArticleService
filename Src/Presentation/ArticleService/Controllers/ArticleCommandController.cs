using Application.Features.Article.Command;
using ArticleService.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.Controllers;

[Route("api/post/v2/articles")]
[ApiController]
public class ArticleCommandController : SubControllerBase
{
    private readonly IMediator _mediator;

    public ArticleCommandController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST /articles
    [HttpPost]
    public async Task<IActionResult> AddPost(
        [FromBody]AddArticleCommand addPostCommand
    ){
        addPostCommand.Sub = Sub;
        var a =await _mediator.Send(addPostCommand);
        return Ok(a);
    }

    // PUT /articles/:id
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(
        [FromRoute] string id,
        [FromBody] UpdateArticleCommand command
    ) {
        command.ID = id;
        command.Sub = Sub;
        //Console.WriteLine("#####################################\n\n\n\n\n");
        //Console.WriteLine(command.ID);
        //Console.WriteLine(command.Sub);
        return Ok(await _mediator.Send(command));
        //return Ok(new { id });
    }

    // PATCH /articles/:id/publish
    // PATCH /articles/:id/unpublish
    // PATCH /articles/:id/like
    // PATCH /articles/:id/unlike
    // PATCH /articles/:id/save
    // PATCH /articles/:id/unsave

    // DELETE /articles/:id
}
