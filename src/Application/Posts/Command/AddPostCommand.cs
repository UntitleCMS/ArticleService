using Application.Common.Extentions;
using Application.Common.Interfaces;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
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
    private readonly IAppDbContext _appDbContext;

    public AddPostCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<string> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        using var transaction = _appDbContext.Database.BeginTransaction();

        try
        {
            var newPost = new Post()
            {
                PostTitle = request.Title,
                Thumbnail = request.Cover,
                Contest = request.Content,
                IsPublished = request.IsPublish,
                OwnerID = request.Sub
            };
            _appDbContext.Attach(newPost);

            var tagsToAdd = await _appDbContext.Tags
                .Where(t => request.TagsId.Contains(t.ID))
                .ToListAsync(cancellationToken);
            newPost.Tags = tagsToAdd;


            _appDbContext.SaveChanges();
            transaction.Commit();
            return newPost.ID.ToBase64Url();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            return e.Message;
        }
    }
}
