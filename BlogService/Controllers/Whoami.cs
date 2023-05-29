using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation.AspNetCore;

namespace AuthenticationService.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class Whoami : ControllerBase
    {
        //[HttpGet]
        public async Task<IActionResult> ME()
        {
            return Ok(new
            {
                //claim = User.Claims.ToList(),
                sub = User.Claims.FirstOrDefault(c=>c.Type=="sub")?.Value,
                name = User.Identity?.Name
            }) ;
        }
    }
}
