using Application.Features.Article.Query;
using ArticleService.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.Controllers;

[Route("api/post/v2/articles")]
[ApiController]
public class ArticleQueryController : SubControllerBase
{
    private readonly IMediator _mediator;

    public ArticleQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET /articles

    // GET /articles/:id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost(
        [FromRoute]string id
    ){
        var query = new GetArticleQuery(id, Sub);
        return Ok(await _mediator.Send(query));
    }
}
