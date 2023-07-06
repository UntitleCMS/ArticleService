using Application.Common.Extentions;
using Application.Common.Interfaces;
using Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Query;

public record GetPostQuery(string Id) : IRequest<IQueryable<Post>>;

public class GetPostQueryHandeler : IRequestHandler<GetPostQuery, IQueryable<Post>>
{
    private readonly IAppDbContext _appDbContext;

    public GetPostQueryHandeler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public Task<IQueryable<Post>> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        var a = _appDbContext.Posts
            .Where(p => p.IsPublished == true)
            .Where(p => p.ID == request.Id.ToGuid())
            .Include(p => p.Tags)
            .Include(p => p.Comments)
            .AsNoTracking();
        return Task.FromResult(a);
    }
}
