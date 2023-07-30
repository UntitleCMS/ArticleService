using MediatR;

namespace Application.Common.Mediator;

public interface IRequestHandlerWithResult<TRequest, TResponse>
    : IRequestHandler<TRequest, IResponseWrapper<TResponse>>
    where TRequest : IRequestWrapper<TResponse>
{
}