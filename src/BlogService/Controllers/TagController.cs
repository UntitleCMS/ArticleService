using Application.Services.TagService;
using Application.Services.TagService.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogService.Controllers
{
    [Route("tags")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly TagServices _tagServices;

        public TagController(TagServices tagServices)
        {
            this._tagServices = tagServices;
        }

        [HttpGet]
        public IActionResult Gets()
        {
            return Ok(_tagServices.Tags.AsNoTracking());
        }

        [HttpGet]
        [Route("~/api/tags-id")]
        public IActionResult GetTagsByID([FromQuery(Name = "id")] ICollection<int> ids)
        {
            return Ok(_tagServices.getTagsById(ids).AsNoTracking());
        }

        [HttpGet]
        [Route("~/api/tags-name")]
        public IActionResult GetTagsByName([FromQuery(Name = "tag")] ICollection<string> name)
        {
            return Ok(_tagServices.getTagsByName(name).AsNoTracking());
        }

        [HttpPost]
        public IActionResult AddTag([FromBody] TagRequestAdd name)
        {
            return Ok(_tagServices.AddTag(name));
        }
    }
}
