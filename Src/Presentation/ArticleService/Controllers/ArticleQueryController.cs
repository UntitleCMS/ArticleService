using Application.Features.Article.Query.GetArticle;
using ArticleService.Common;
using ArticleService.Common.Mapper;
using ArticleService.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;

namespace ArticleService.Controllers;

[Route("articles")]
[ApiController]
public class ArticleQueryController : SubControllerBase
{
    private readonly IMediator _mediator;

    public ArticleQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET /articles
    [HttpGet]
    public async Task<IActionResult> GetArticles(ArticlesQueryDto a)
    {
        Console.WriteLine(a.ToJson(new() { Indent = true}));
        if (!a.SerchText.IsNullOrEmpty())
            return Ok(await _mediator.Send(a.GetSerchArticleQuery()));
        return Ok(await _mediator.Send(a.GetQuery()));
    }

    // GET /articles/:id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost(
        [FromRoute]string id
    ){
        var query = new GetArticleQuery(id, Sub);
        return Ok(await _mediator.Send(query));
    }
}
