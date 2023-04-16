using BlogService.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogService.Controllers
{
    [Route("blog-service/api/v1/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public PostsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var posts = _db.Posts.ToList();
            return Ok(posts);
        }
    }
}
