using Application.Common.Interfaces.Repositoris;
using Application.Common.Mediator;
using Domain.Entity;

namespace Application.Posts.Command;

public class DeletePostCommand : IRequestWrapper<string>
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
}

public class DeletePostCommandHandler : IRequestHandlerWithResult<DeletePostCommand, string>
{
    private readonly IRepositoryRemover<Post,Guid> _repository;
    public DeletePostCommandHandler(IRepositoryRemover<Post, Guid> repository)
    {
        _repository = repository;
    }

    public Task<IResponseWrapper<string>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _repository .DeleteWithAuthority(request.PostId, request.UserId);
            _repository.SaveChangesAsync(cancellationToken);
        }catch (Exception e)
        {
            return Task.FromResult(Response.Fail<string>(e));
        }
        return Task.FromResult(Response.Ok(""));
    }
}
