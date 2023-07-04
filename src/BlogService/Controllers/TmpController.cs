using Application.Posts.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TmpController : ControllerBase
{
    private readonly IMediator _mediator;
    public TmpController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public IActionResult tmp()
    {
        return new OkObjectResult( _mediator.Send(new GetAllPostsQuery()).Result );
    }
}
