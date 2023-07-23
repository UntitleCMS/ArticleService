using Application.Common.Extentions;
using Application.Common.Interfaces;
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
    private readonly IAppMongoDbContext _mongoDb;
    private readonly IRepository<Post, Guid> _post;

    public DbPostsController(IAppMongoDbContext mongoDb, IRepository<Post, Guid> post)
    {
        _mongoDb = mongoDb;
        _post = post;
    }

    [HttpGet("posts")]
    public IActionResult GetPosts()
    {
        _post.Add(new()
        {
            PostTitle = "hello",
        });
        //_post.SaveChanges();
        var a = _post.ToArray();
       return Ok(a);
    }

}
