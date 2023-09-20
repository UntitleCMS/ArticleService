using Application.Common.Interfaces;
using Application.Common.models;
using Application.Common.Repositories;
using MediatR;

namespace Application.Features.Article.Command.LikeArticle;

using ResponseType = IResponseWrapper<string>;
using HandlerType = IRequestHandler<LikeArticleCommand, IResponseWrapper<string>>;

public class LikeArticleCommandHandeler : HandlerType
{
    private readonly IPostRepository _repo;

    public LikeArticleCommandHandeler(IPostRepository repo)
    {
        _repo = repo;
    }

    public Task<ResponseType> Handle(LikeArticleCommand request, CancellationToken cancellationToken)
    {
        return Like(request.articleId, request.sub);
    }

    private Task<ResponseType> Like(string id, string sub)
    {
        var res = _repo.Like(id, sub);

        if (res.IsFaulted && res.Exception is not null)
            return Task.FromResult<ResponseType>(
                ResponseWrapper.Error<string>(res.Exception, $"You are unsuccess to lik article : {id}"));

        return Task.FromResult<ResponseType>(
            ResponseWrapper.Ok($"You are success to lik article : {id}"));
    }
}
