using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Common.Queues;
using Microsoft.IdentityModel.Tokens;

namespace Application.Events.NewArticle;

public class NewArticleEventHandler : RequestPipeHandelerBase<NewArticleEvent, string>
{
    private readonly INewArticleQueue queue;

    public NewArticleEventHandler(INewArticleQueue queue)
    {
        this.queue = queue;
    }

    protected override string DefaultErrorMessage => "Emit event : New Article Error";

    protected override async Task<IResponseWrapper<string>> Execute(NewArticleEvent request)
    {
        Console.WriteLine("NEW POST EVENT ");
        var data = new
        {
            PostID = Base64UrlEncoder.Encode(request.post.ID.ToByteArray()),
            AuthorID = request.post.AuthorId,
            request.post.CreatedAt
        };
        queue.Publish(data);

        return Ok(string.Empty);
    }
}
