using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace ArticleService.Common;

public class SubControllerBase : ControllerBase
{
    public string? Sub
    {
        get
        {
            if (HttpContext.Request.Query.TryGetValue("sub", out StringValues sub))
                return sub.ToString();
            else return null;
        }
    }
    public SubControllerBase() : base()
    {
    }
}
