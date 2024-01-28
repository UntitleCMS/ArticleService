using MediatR;
using Application.Common.Interfaces;
using Application.Common.models;
using Application.Common.Repositories;
using Microsoft.IdentityModel.Tokens;
using Application.Events.NewArticle;

namespace Application.Features.Article.Command.AddArticle;

using AddPostCommandResponseType = IResponseWrapper<string>;
using AddPostCommandHandlerType = IRequestHandler<AddArticleCommand, IResponseWrapper<string>>;

public class AddArticleCommandHandler : AddPostCommandHandlerType
{
    private readonly IPostRepository postRepo;
    private readonly IMediator mediator;

    public AddArticleCommandHandler(IPostRepository postRepo, IMediator mediator)
    {
        this.postRepo = postRepo;
        this.mediator = mediator;
    }

    public async Task<AddPostCommandResponseType> Handle(AddArticleCommand request, CancellationToken cancellationToken)
    {
        var v = request.Validate();
        if (v is not null)
        {
            return ResponseWrapper.Error<string>(v, "Data validation fail");
        }

        var post = request.PostEntity;
        var a = postRepo.Add(ref post, cancellationToken);

        if (a.IsCompletedSuccessfully)
        {
            _ = mediator.Send(new NewArticleEvent(post), cancellationToken);

            return ResponseWrapper.Ok(
                Base64UrlEncoder.Encode(post.ID.ToByteArray()),
                "Add post successfuly.");
        }

        return ResponseWrapper.Error<string>(a.Exception, "Error occur when save to database");

    }

}
