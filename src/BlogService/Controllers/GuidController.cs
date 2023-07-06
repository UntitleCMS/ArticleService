using Application.Common.Extentions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidController : ControllerBase
    {
        [HttpGet] public IActionResult Get()
        {
            var a = Guid.NewGuid();
            return Ok(new
            { 
                Long = a.ToString(),
                Short = a.ToBase64Url()
            });
        }

        [HttpPost] public IActionResult Convert(Guid? Long, string? Short)
        {
            return Ok(new
            {
                Long = Long?.ToBase64Url(),
                Short = Short?.ToGuid()
            }) ;
        }
    }
}
