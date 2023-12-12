using Application.Features.Tag.Query.GetTags;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.Controllers;

[Route("tags")]
[ApiController]
public class TagQueryController : ControllerBase
{
    private IMediator _mediator;

    public TagQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET Top Tags
    [HttpGet]
    public async Task<ActionResult> GetTopTags(int n = 10)
    {
        var a = await _mediator.Send(new GetTagsQuery(n==0? 10 : n));
        return Ok(a);
    }

}
