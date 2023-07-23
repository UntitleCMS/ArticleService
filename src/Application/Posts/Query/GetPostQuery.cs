using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositoris;
using Application.Posts.Dto;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Posts.Query;

public record GetPostQuery(string Id) : IRequest<PostDto>;

public class GetPostQueryHandeler : IRequestHandler<GetPostQuery, PostDto>
{
    private readonly IAppMongoDbContext _appDbContext;
    private readonly IRepository<Post, Guid> _postRepository;

    public GetPostQueryHandeler(
        IAppMongoDbContext appDbContext,
        IRepository<Post, Guid> postRepository)
    {
        _appDbContext = appDbContext;
        _postRepository = postRepository;
    }

    public async Task<PostDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        var id = request.Id.ToGuid();
        var a = _postRepository
            .Where(p => p.IsPublished == true)
            .Where(p => p.ID == id)
            .FirstOrDefault();
        return new PostDto(a!)!;
    }
}
