using Infrastructure.Data;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TmpController : ControllerBase
    {

        private readonly AppDbContext _db;

        public TmpController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("Add")]
        public IActionResult AddCollumn()
        {
            var data = new Post()
            {
                OwnerID = "97f16679-1a2b-4a23-9b7a-3c3bc8c54f3d",
                PostID = Guid.NewGuid(), 
                PostTitle = "Title",
                Contest = "contents",
                Thumbnail = "tm"
            };

            _db.Posts.Add(data);
            _db.SaveChanges();

            return Ok();
        }

        [HttpGet("First")]
        public IActionResult First()
        {
            var data = _db.Posts.FirstOrDefault();

            return Ok(data);
        }

        [HttpGet("Update")]
        public IActionResult EditCollumn()
        {
            var data = _db.Posts.FirstOrDefault(); 
            if (data == null) 
                return NotFound();

            data.Thumbnail = Guid.NewGuid().ToString();
            _db.Update(data);
            _db.SaveChanges();

            return Ok(data);
        }
    }
}
