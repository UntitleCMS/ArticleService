using ArticleService.Common;
using ArticleService.Common.Mapper;
using ArticleService.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.Controllers;

[Route("darfts")]
[ApiController]
public class MyDarftController : SubControllerBase
{
    private readonly IMediator _mediator;

    public MyDarftController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> GetMyDarfts(ArticlesQueryDto dto)
    {
        var x = dto.GetQuery();
        x.IsPublish = false;
        return Ok(await _mediator.Send(x));
    }
}
