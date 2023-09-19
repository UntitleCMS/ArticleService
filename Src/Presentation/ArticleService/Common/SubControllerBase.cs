using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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

    public override OkObjectResult Ok([ActionResultObjectValue] object? value)
    {
        if (value is IResponseWrapper responseWrapper)
        {
            if (responseWrapper.IsSuccess)
            {
                return base.Ok(value);
            }
            else
            {
                return base.Ok(new
                {
                    message = responseWrapper.Message,
                    isSuccess = false,
                    errors = GetExceptionMessages(responseWrapper.Error)
                });
            }
        }
        return base.Ok(value);
    }

    private string[]? GetExceptionMessages(Exception? e)
    {
        if (e is null)
            return null;

        if (e is AggregateException aggregateException)
        {
            return GetExceptionMessages(aggregateException);
        }
        return new[] { e.Message };
    }

    private string[]? GetExceptionMessages(AggregateException e)
    {
        var err = e.InnerException;
        return GetExceptionMessages(err);
    }
}
