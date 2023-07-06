using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Posts.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        ID = id.ToGuid();
        Direction = get;
        IsPage = true;
    }

    public GetAllPostsQuery() => IsPage = false;

}

public class GetAllPostsQueryHandeler : IRequestHandler<GetAllPostsQuery, IQueryable<PostDto>>
{
    private readonly IAppDbContext _appDbContext;

    public GetAllPostsQueryHandeler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public Task<IQueryable<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        var postQurey = _appDbContext.Posts
            .Where(p => p.IsPublished == true);

        if (request.IsPage)
        {
            if (request.Direction == "BEFORE")
                postQurey = postQurey
                    .Where(p => p.CreatedAt < _appDbContext.Posts.FirstOrDefault(r => r.ID == request.ID).CreatedAt)
                    .OrderByDescending(p=>p.CreatedAt);
            else
                postQurey = postQurey
                    .Where(p => p.CreatedAt > _appDbContext.Posts.FirstOrDefault(r => r.ID == request.ID).CreatedAt)
                    .OrderBy(p=>p.CreatedAt);
        }
        else
            postQurey = postQurey.OrderBy(p => p.CreatedAt);


        var a = postQurey
            .Include(p => p.Tags)
            .Take(request.Take)
            .Select(p => new PostDto(p));

        return Task.FromResult(a);
    }
}
