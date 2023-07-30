using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositoris;
using Application.Common.Mediator;
using Domain.Entity;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Posts.Command;

public class AddPostCommand : IRequestWrapper<string>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CoverImage { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string[] Tags { get; set; } = Array.Empty<string>();
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

    public async Task<IResponseWrapper<string>> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            Title = request.Title,
            ContentPreviews = request.Description,
            Cover = request.CoverImage,
            Content = request.Content,
            IsPublished = request.IsPublish,
            Author = new Guid(request.Sub),
            CreatedAt = DateTime.Now,
            LastUpdated = DateTime.Now,
            Tags = request.Tags
        };

        _postRepository.Add(post);
        await _postRepository.SaveChangesAsync(cancellationToken);

        return Response.Ok(post.ID.ToBase64Url());
    }
}
