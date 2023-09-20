using MediatR;
using Application.Common.Interfaces;
using Application.Common.models;
using Application.Common.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Article.Command.AddArticle;

using AddPostCommandResponseType = IResponseWrapper<string>;
using AddPostCommandHandlerType = IRequestHandler<AddArticleCommand, IResponseWrapper<string>>;

public class AddArticleCommandHandler : AddPostCommandHandlerType
{
    private readonly IPostRepository postRepo;

    public AddArticleCommandHandler(IPostRepository postRepo)
    {
        this.postRepo = postRepo;
    }

    public async Task<AddPostCommandResponseType> Handle(AddArticleCommand request, CancellationToken cancellationToken)
    {
        var v = request.Validate();
        if (v is not null)
        {
            return ResponseWrapper.Error<string>(v, "Data validation fail");
        }

        var post = request.PostEntity;
        var a = postRepo.SavePost(ref post, cancellationToken);

        if (a.IsCompletedSuccessfully)
        {
            return ResponseWrapper.Ok(
                Base64UrlEncoder.Encode(post.ID.ToByteArray()),
                "Add post successfuly.");
        }

        return ResponseWrapper.Error<string>(a.Exception, "Error occur when save to database");

    }

}
