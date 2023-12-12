using Application.Common.Interfaces;
using Application.Common.models;
using MediatR;

namespace Application.Common.Mediator;

public abstract class RequestPipeHandelerBase<TReq, TRes> : IRequestHandler<TReq, IResponseWrapper<TRes>>
    where TReq : IRequestWraped<TRes>
{
    protected abstract string DefaultErrorMessage { get; }

    public async Task<IResponseWrapper<TRes>> Handle(TReq request, CancellationToken cancellationToken)
    {
        return await Execute(request);
    }

    protected abstract Task<IResponseWrapper<TRes>> Execute(TReq request);

    protected IResponseWrapper<TRes> Ok(TRes data)
    {
        return ResponseWrapper.Ok(data);
    }
    protected IResponseWrapper<TRes> Error(Exception e)
    {
        return ResponseWrapper.Error<TRes>(e,DefaultErrorMessage);
    }
}
