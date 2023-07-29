using Application.Common.Extentions;
using Application.Common.Interfaces.Repositoris;
using Application.Common.Mediator;
using Application.Common.Models;
using Application.Posts.Dto;
using Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Application.Posts.Query;

public class GetAllPostsQuery : IRequestWrap<PageableWrapper<PostDto>>
{
    public virtual int Take { get; set; } = 20;
    public virtual Guid? RefPostId { get; set; } = default;
}

public class GetAllPostsQueryHandeler : IRequestHandlerWithResult<GetAllPostsQuery, PageableWrapper<PostDto>>
{
    public readonly IRepositoryPageable<Post, Guid> _postReposirory;

    public GetAllPostsQueryHandeler(
        IRepositoryPageable<Post, Guid> postReposirory)
    {
        _postReposirory = postReposirory;
    }

    public async Task<IResponse<PageableWrapper<PostDto>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<PostDto> res = null!;

        if (request.Take > 0)
        {
            res = _postReposirory
                .GetBefore(request.Take, request.RefPostId)
                .Select(p => new PostDto(p));
        }
        else if (request.Take < 0)
        {
            res = _postReposirory
                .GetAfter(-1 * request.Take, request.RefPostId)
                .Select(p => new PostDto(p));
        }
        else
        {
            res = Enumerable.Empty<PostDto>();
        }

        var rapper = new PageableWrapper<PostDto>()
        {
            Data = res.Take(Math.Abs(request.Take)),
        };
        rapper.HasNext = res.Count() - 1 == rapper.Data.Count();

        return Response.Ok(rapper);
    }

}
