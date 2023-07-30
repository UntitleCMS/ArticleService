using Application.Common.Interfaces.Repositoris;
using Application.Common.Mediator;
using Domain.Entity;
using Newtonsoft.Json;

namespace Application.Posts.Command;

public class UpdatePostCommand : IRequestWrapper<string>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public string CoverImage { get; set; } = string.Empty;
    public IList<string>? Tags { get; set; }

    [JsonIgnore]
    public Guid PostId { get; set; }
    [JsonIgnore]
    public Guid AuthorId { get; set; }
}


public class UpdatePostCommandHandler : IRequestHandlerWithResult<UpdatePostCommand, string>
{
    private readonly IRepository<Post, Guid> _post;

    public UpdatePostCommandHandler(IRepository<Post, Guid> post)
    {
        _post = post;
    }

    public async Task<IResponseWrapper<string>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _post.Update(new()
            {
                ID = request.PostId,
                Author = request.AuthorId,
                Title = request.Title,
                ContentPreviews = request.Description,
                Content = request.Content,
                IsPublished = request.IsPublished,
                Cover = request.CoverImage,
                Tags = request.Tags
            });
            await _post.SaveChangesAsync(cancellationToken);
        } catch (Exception ex)
        {
            return Response.Fail<string>(ex);
        }
        return Response.Ok("");
    }
}
