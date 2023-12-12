using Application.Features.Bookmark.Query;
using ArticleService.Common;
using ArticleService.Common.Mapper;
using ArticleService.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.Controllers;

[Route("bookmark")]
[ApiController]
public class BookmarkQueryController : SubControllerBase
{
    private IMediator _mediator;

    public BookmarkQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> GetMyBookmark(ArticlesQueryDto dto)
    {
        var x = await _mediator.Send(dto.GetMyBookmarksQuery());
        return Ok(x);
        //return Ok();
    }
}

