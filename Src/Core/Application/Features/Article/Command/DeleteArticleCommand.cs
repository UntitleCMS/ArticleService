using Application.Common.Interfaces;
using Application.Common.models;
using Application.Common.Repositories;
using MediatR;

namespace Application.Features.Article.Command;

using ResponseType = IResponseWrapper<string>;
using CommandType = IRequest<IResponseWrapper<string>>;
using HandlerType = IRequestHandler<DeleteArticleCommand, IResponseWrapper<string>>;

public record DeleteArticleCommand(string articleId, string? sub = default) : CommandType;

public class DeleteArticleCommandHandeler : HandlerType
{
    public readonly IPostRepository _repo;

    public DeleteArticleCommandHandeler(IPostRepository repo)
    {
        _repo = repo;
    }

    public Task<ResponseType> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var res = _repo.Delete(request.articleId, request.sub, cancellationToken);

        if (res.IsFaulted)
            return Task.FromResult<ResponseType>(
                ResponseWrapper.Error<string>(res.Exception));

        return Task.FromResult<ResponseType>(ResponseWrapper.Ok(""));
    }
}
