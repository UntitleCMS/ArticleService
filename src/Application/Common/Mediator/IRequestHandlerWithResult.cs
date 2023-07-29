using MediatR;

namespace Application.Common.Mediator;

public interface IRequestHandlerWithResult<TRequest, TResponse>
    : IRequestHandler<TRequest, IResponse<TResponse>>
    where TRequest : IRequestWrap<TResponse>
{
}