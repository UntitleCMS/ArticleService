using BlogService.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogService.Controllers
{
    [Route("blog-service/api/v1/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly AppDbContext _db;

        public PostController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("{postId}")]
        public IActionResult Get( [FromRoute()] Guid postId)
        {
            var posts = _db.Posts
                .Where(p=>p.PostID==postId)
                .FirstOrDefault();
            if (posts == null)
                return NotFound();

            return Ok(posts);
        }
    }
}
