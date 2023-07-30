using Application.Common.Extentions;
using Application.Common.Interfaces.Repositoris;
using Application.Common.Mediator;
using Application.Posts.Dto;
using Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Application.Posts.Query;

public class GetPostDetailQuery : IRequestWrapper<PostDto>
{
    public virtual string Id { get; set; } = string.Empty;
}

public class GetPostDetailQueryHandeler : IRequestHandlerWithResult<GetPostDetailQuery, PostDto>
{
    private readonly IRepository<Post, Guid> _postRepository;

    public GetPostDetailQueryHandeler( IRepository<Post, Guid> postRepository)
    {
        _postRepository = postRepository;
    }

    public Task<IResponseWrapper<PostDto>> Handle(GetPostDetailQuery request, CancellationToken cancellationToken)
    {
        var post = _postRepository.Find(request.Id.ToGuid());
        if (post is null)
            return Task.FromResult(Response.Fail<PostDto>( new NullReferenceException()));

        var res = Response.Ok(new PostDto(post));
        Console.WriteLine(res.Data);
        return Task.FromResult(res);
    }
}
