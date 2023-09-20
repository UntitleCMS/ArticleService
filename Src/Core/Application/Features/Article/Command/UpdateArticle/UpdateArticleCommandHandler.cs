using MediatR;
using Application.Common.Interfaces;
using Application.Common.models;
using Application.Common.Repositories;

namespace Application.Features.Article.Command.UpdateArticle;

using ResponseType = IResponseWrapper<string>;
using HandlerType = IRequestHandler<UpdateArticleCommand, IResponseWrapper<string>>;

public class UpdateArticleCommandHandler : HandlerType
{
    private readonly IPostRepository postRepo;

    public UpdateArticleCommandHandler(IPostRepository postRepo)
    {
        this.postRepo = postRepo;
    }

    public async Task<ResponseType> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var v = request.Validate();
        if (v is not null)
        {
            return ResponseWrapper.Error<string>(v, "Data validation fail");
        }

        var post = request.PostEntity;
        var a = postRepo.Update(post, cancellationToken);

        if (a.IsCompletedSuccessfully)
        {
            return ResponseWrapper.Ok(
                request.ID,
                "Update post successfuly.");
        }

        return ResponseWrapper.Error<string>(a.Exception, "Error occur when save to database");

    }

}
