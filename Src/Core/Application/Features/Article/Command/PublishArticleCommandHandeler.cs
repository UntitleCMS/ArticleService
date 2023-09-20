using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Common.Repositories;

namespace Application.Features.Article.Command;

using ResponseType = String;

public class PublishArticleCommandHandeler : RequestPipeHandelerBase<PublishArticleCommand, ResponseType>
{
    private readonly IPostRepository _repo;
    protected override string DefaultErrorMessage => "Publish unsuccess";

    public PublishArticleCommandHandeler(IPostRepository repo)
    {
        _repo = repo;
    }


    protected override async Task<IResponseWrapper<string>> Execute(PublishArticleCommand request)
    {
        await _repo.Publish(request.id,request.sub);
        return Ok("Publish success");
    }
}
