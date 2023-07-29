using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositoris;
using Application.Common.Mediator;
using Domain.Entity;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Posts.Command;

public class AddPostCommand : IRequestWrap<string>
{
    public string Title { get; set; } = string.Empty;
    public string SubTitle { get; set; } = string.Empty;
    public string Cover { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string[] TagsId { get; set; } = Array.Empty<string>();
    public bool IsPublish { get; set; } = false;

    [JsonIgnore]
    public string Sub { get; set; } = string.Empty;
}

public class AddPostCommandHandler : IRequestHandlerWithResult<AddPostCommand, string>
{
    private readonly IRepository<Post,Guid> _postRepository;

    public AddPostCommandHandler(
        IRepository<Post, Guid> postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IResponse<string>> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            Title = request.Title,
            ContentPreviews = request.SubTitle,
            Cover = request.Cover,
            Content = request.Content,
            IsPublished = request.IsPublish,
            Author = new Guid(request.Sub),
            CreatedAt = DateTime.Now,
            LastUpdated = DateTime.Now,
            Tags = request.TagsId
        };

        _postRepository.Add(post);
        await _postRepository.SaveChangesAsync();

        return Response.Ok(post.ID.ToBase64Url());
    }
}
