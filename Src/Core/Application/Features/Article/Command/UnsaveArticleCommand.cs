using Application.Common.Interfaces;
using Application.Common.models;
using Application.Common.Repositories;
using MediatR;

namespace Application.Features.Article.Command;

using ResponseType = IResponseWrapper<string>;
using CommandType = IRequest<IResponseWrapper<string>>;
using HandlerType = IRequestHandler<UnsaveArticleCommand, IResponseWrapper<string>>;

public record UnsaveArticleCommand(string articleId, string sub) : CommandType;

public class UnsaveArticleCommandHandeler : HandlerType
{
    private readonly IPostRepository _repo;

    public UnsaveArticleCommandHandeler(IPostRepository repo)
    {
        _repo = repo;
    }

    public Task<ResponseType> Handle(UnsaveArticleCommand request, CancellationToken cancellationToken)
    {
        return Like(request.articleId, request.sub);
    }

    private Task<ResponseType> Like(string id, string sub)
    {
        var res =  _repo.UnSave(id, sub);

        if (res.IsFaulted && res.Exception is not null)
            return Task.FromResult<ResponseType>(
                ResponseWrapper.Error<string>(res.Exception ,$"You are unsuccess to lik article : {id}"));

        return Task.FromResult<ResponseType>(
            ResponseWrapper.Ok($"You are success to lik article : {id}"));
    } 
}
