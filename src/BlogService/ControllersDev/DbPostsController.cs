using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositoris;
using Domain.Entity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogService.ControllersDev;


// MongoFramework is not require for prod but dev 
[Route("mongo-db")]
[ApiController]
public class DbPostsController : ControllerBase
{
    private readonly IRepository<Post, Guid> _post;
    private readonly IRepository<Post, Guid> _newpost;

    public DbPostsController(IRepository<Post, Guid> post, IRepository<Post, Guid> newpost)
    {
        _post = post;
        _newpost = newpost;
    }

    [HttpGet("posts")]
    public IActionResult GetPosts()
    {
       return Ok(_newpost.Find("QPoZpAIMq0SQp0O5RaZKhQ".ToGuid()));
    }

}
