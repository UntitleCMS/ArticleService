using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositoris;
using Domain.Entity;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using System.Text.Json.Serialization;

namespace Application.Posts.Command;

public class AddPostCommand : IRequest<string>
{
    public string Title { get; set; } = string.Empty;
    public string SubTitle { get; set; } = string.Empty;
    public string Cover { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int[] TagsId { get; set; } = Array.Empty<int>();
    public bool IsPublish { get; set; }

    [JsonIgnore]
    public string Sub { get; set; } = string.Empty;
}

public class AddPostCommandHandler : IRequestHandler<AddPostCommand, string>
{
    private readonly IAppMongoDbContext _appDbContext;
    private readonly IRepository<Post,Guid> _postRepository;

    public AddPostCommandHandler(
        IAppMongoDbContext appDbContext,
        IRepository<Post, Guid> postRepository)
    {
        _appDbContext = appDbContext;
        _postRepository = postRepository;
    }

    public async Task<string> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            PostTitle = request.Title,
            PostSubTitle = request.SubTitle,
            Thumbnail = request.Cover,
            Contest = request.Content,
            IsPublished = request.IsPublish,
            OwnerID = new Guid(request.Sub),
            CreatedAt = DateTime.Now,
            LastUpdated = DateTime.Now,
            Tags = new List<string>()
            {
                "html","css"
            }
        };

        _postRepository.Add(post);
        await _postRepository.SaveChangesAsync();

        return post.ID.ToBase64Url();
    }
}
