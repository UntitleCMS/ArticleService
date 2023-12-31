using Application.Features.Tag.Query.GetTags;
using Application.Features.Tag.Query.SearchTags;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
    public async Task<ActionResult> GetTopTags(string lookLike = "", int n = 10)
    {
        if (lookLike.IsNullOrEmpty())
        {
            var t = await _mediator.Send(new GetTagsQuery(n == 0 ? 10 : n));
            return Ok(t);
        }

        var s = await _mediator.Send(new SerchTagsQuery(lookLike, n == 0 ? 10 : n));
        return Ok(s);
    }

}
