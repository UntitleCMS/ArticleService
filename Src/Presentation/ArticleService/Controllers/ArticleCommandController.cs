using Application.Features.Article.Command.AddArticle;
using Application.Features.Article.Command.DeleteArticle;
using Application.Features.Article.Command.LikeArticle;
using Application.Features.Article.Command.PublishArticle;
using Application.Features.Article.Command.SavaArticle;
using Application.Features.Article.Command.UnlikeArticle;
using Application.Features.Article.Command.UnPublish;
using Application.Features.Article.Command.UnSaveArticle;
using Application.Features.Article.Command.UpdateArticle;
using ArticleService.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.Controllers;

[Route("articles")]
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
        return Ok(await _mediator.Send(command));
    }

    // PATCH /articles/:id/publish
    [HttpPatch("{id}/publish")]
    public async Task<IActionResult> Publish([FromRoute]string id)
    {
        var res = await _mediator.Send(new PublishArticleCommand(id,Sub));
        return Ok(res);
    }

    // PATCH /articles/:id/unpublish
    [HttpPatch("{id}/unpublish")]
    public async Task<IActionResult> Unpublish([FromRoute]string id)
    {
        var res = await _mediator.Send(new UnpublishArticleCommand(id,Sub));
        return Ok(res);
    }

    // PATCH /articles/:id/like
    [HttpPatch("{id}/like")]
    public async Task<IActionResult> Like([FromRoute] string id)
    {
        var res = await _mediator.Send(new LikeArticleCommand(id,Sub));
        return Ok(res);
    }

    // PATCH /articles/:id/unlike
    [HttpPatch("{id}/unlike")]
    public async Task<IActionResult> Unlike([FromRoute] string id)
    {
        var res = await _mediator.Send(new UnlikeArticleCommand(id,Sub));
        return Ok(res);
    }
    // PATCH /articles/:id/save
    [HttpPatch("{id}/save")]
    public async Task<IActionResult> Save([FromRoute] string id)
    {
        var res = await _mediator.Send(new SaveArticleCommand(id,Sub));
        return Ok(res);
    }

    // PATCH /articles/:id/unsave
    [HttpPatch("{id}/unsave")]
    public async Task<IActionResult> UnSave([FromRoute] string id)
    {
        var res = await _mediator.Send(new UnsaveArticleCommand(id,Sub));
        return Ok(res);
    }

    // DELETE /articles/:id
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        [FromRoute ] string id
    ){
        var command = new DeleteArticleCommand(id,Sub);
        return Ok(await _mediator.Send(command));
    }
}
