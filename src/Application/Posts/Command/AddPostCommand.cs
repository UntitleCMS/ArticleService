using Application.Common.Extentions;
using Application.Common.Interfaces;
using Domain.Entity;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Posts.Command;

public class AddPostCommand : IRequest<string>
{
    public string Title { get; set; } = string.Empty;
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

    public AddPostCommandHandler(IAppMongoDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<string> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        _appDbContext.Posts.Add(new()
        {
            PostTitle = request.Title,
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
        });
        await _appDbContext.SaveChangesAsync();
        return "";
        //try
        //{
        //    var newPost = new Post()
        //    {
        //        PostTitle = request.Title,
        //        Thumbnail = request.Cover,
        //        Contest = request.Content,
        //        IsPublished = request.IsPublish,
        //        OwnerID = new Guid(request.Sub)
        //    };
        //    _appDbContext.Attach(newPost);

        //    var tagsToAdd = await _appDbContext.Tags
        //        .Where(t => request.TagsId.Contains(t.ID))
        //        .ToListAsync(cancellationToken);
        //    newPost.Tags = tagsToAdd;


        //    _appDbContext.SaveChanges();
        //    return newPost.ID.ToBase64Url();
        //}
        //catch (Exception e)
        //{
        //    return e.Message;
        //}
    }
}
