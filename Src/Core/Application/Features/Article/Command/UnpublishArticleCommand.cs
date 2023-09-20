using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Common.Repositories;

namespace Application.Features.Article.Command;

public record UnpublishArticleCommand(string id, string sub) : IRequestWraped<string>;

public class UnpublishArticleCommandHandeler : RequestPipeHandelerBase<UnpublishArticleCommand, string>
{
    private readonly IPostRepository _repo;
    protected override string DefaultErrorMessage => "Un publish fail";

    public UnpublishArticleCommandHandeler(IPostRepository repo)
    {
        _repo = repo;
    }


    protected override async Task<IResponseWrapper<string>> Execute(UnpublishArticleCommand request)
    {
        await _repo.Unpublish(request.id, request.sub);
        return Ok("Un published");
    }
}
