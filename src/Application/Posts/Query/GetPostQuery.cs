using Application.Common.Extentions;
using Application.Common.Interfaces;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Query;

public record GetPostQuery(string Id) : IRequest<IQueryable<Post>>;

public class GetPostQueryHandeler : IRequestHandler<GetPostQuery, IQueryable<Post>>
{
    private readonly IAppMongoDbContext _appDbContext;

    public GetPostQueryHandeler(IAppMongoDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IQueryable<Post>> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        var id = request.Id.ToGuid();
        var a = _appDbContext.Posts
            .AsNoTracking()
            .Where(p => p.IsPublished == true)
            .Where(p => p.ID == id);
        return a;
    }
}
