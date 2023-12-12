using Application.Common.Interfaces;
using Application.Common.models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Mediator;

public class ExceptionTransformPipelineBehevior<Q, S> : IPipelineBehavior<Q, S>
    where Q : IRequest<S>
    where S : IResponseWrapper
{
    private readonly ILogger<ExceptionTransformPipelineBehevior<Q, S>> _logger;

    public ExceptionTransformPipelineBehevior(ILogger<ExceptionTransformPipelineBehevior<Q, S>> logger)
    {
        _logger = logger;
    }

    public async Task<S> Handle(Q request, RequestHandlerDelegate<S> next, CancellationToken cancellationToken)
    {
        try
        {
            var res = await next();
            return res;
        }
        catch (Exception ex)
        {
            var res = Error();
            res.Error = ex;
            res.Message = ex.Message;
            res.IsSuccess = false;
            return res;
        }
    }

    private static S Error()
    {
        Type concreteType = typeof(ResponseWrapper<>).MakeGenericType(typeof(object));

        if (typeof(S).IsGenericType)
        {
            Type genericTypeArgument = typeof(S).GenericTypeArguments[0];
            concreteType = typeof(ResponseWrapper<>).MakeGenericType(genericTypeArgument);
        }

        object res = Activator.CreateInstance(concreteType) ?? throw new Exception();
        return (S)res;
    }
}
