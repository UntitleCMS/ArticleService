using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Posts.Dto;
using Domain.Entity;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Query;
public class GetAllPostsQuery : IRequest<IQueryable<PostDto>>
{
    public int Take { get; set; } = 20;
    public Guid ID { get; set; } = default;
    [RegularExpression("BEFORE+AFTER", ErrorMessage = """Only "BEFORE" or "AFTER".""")]
    public string Direction { get; set; }
    public bool IsPage { get; set; }

    public GetAllPostsQuery(string get, string id, int? take = null)
    {
        Take = take ?? Take;
        Direction = get;
        IsPage = true;
        try
        {
            ID = new Guid(id);
        }
        catch (FormatException)
        {
            ID = id.ToGuid();
        }
    }

    public GetAllPostsQuery() => IsPage = false;

}

public class GetAllPostsQueryHandeler : IRequestHandler<GetAllPostsQuery, IQueryable<PostDto>>
{
    private readonly IAppMongoDbContext _appDbContext;

    public GetAllPostsQueryHandeler(
        IAppMongoDbContext appDbContext
        )
    {
        _appDbContext = appDbContext;
    }

    public async Task<IQueryable<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        //_logger.LogInformation("Handle Request : {}", JsonConvert.SerializeObject(request,Formatting.Indented));

        GetPublicedPosts(out var publichedPosts);

        if (request.IsPage)
            SortPost(request.Direction, request.ID, ref publichedPosts);
        else
            publichedPosts = publichedPosts.OrderBy(p => p.CreatedAt);

        var a = publichedPosts
            .Take(request.Take)
            .ToList()
            .Select(p => new PostDto(p));

        //_logger.LogInformation("return data : {}", JsonConvert.SerializeObject(a.Select(p=>new {p.Id,p.OwnerId,p.Title}),Formatting.Indented));
        return a.AsQueryable();

    }


    private void GetPublicedPosts(out IQueryable<Post> publichedPosts )
    {
        publichedPosts = _appDbContext.Posts
            .Where(p => p.IsPublished == true);
    }

    private bool GetRefDate(Guid fromPostId, out DateTime? refDate)
    {
        refDate = _appDbContext.Posts
                .Where(p => p.ID == fromPostId)
                .Select(p => p.CreatedAt)
                .FirstOrDefault();

        if (refDate == DateTime.MinValue)
        {
            //_logger.LogInformation("post ID {} is not found.", fromPostId);
            refDate = null;
            return false;
        }
        return true;

    }

    private void SortPost(string direction, Guid refId, ref IQueryable<Post> posts)
    {
        if (!GetRefDate(refId, out var refDate))
        {
            posts = (IQueryable<Post>) Array.Empty<PostDto>().AsQueryable();
            return;
        }

        if (direction == "BEFORE")
            posts = posts 
                .Where(p => p.CreatedAt < refDate)
                .OrderByDescending(p=>p.CreatedAt);
        else
            posts = posts 
                .Where(p => p.CreatedAt > refDate)
                .OrderBy(p=>p.CreatedAt);
    }

}
