using Application.Common.Interfaces;
using MediatR;

namespace Application.Common.Mediator;

public interface IRequestWraped<TRes>: IRequest<IResponseWrapper<TRes>>
{
}
