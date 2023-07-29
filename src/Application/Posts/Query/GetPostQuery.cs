using Application.Common.Extentions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositoris;
using Application.Common.Mediator;
using Application.Posts.Dto;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Posts.Query;

public class GetPostQuery : IRequestWrap<PostDto>
{
    [RegularExpression(@"^[A-Za-z0-9_-]{22}$", ErrorMessage = "Invalid characters used.")]
    public virtual string Id { get; set; } = string.Empty;
    public GetPostQuery() { }
    public GetPostQuery(string Id)
    {
        this.Id = Id;
    }
}

public class GetPostQueryHandeler : IRequestHandlerWithResult<GetPostQuery, PostDto>
{
    private readonly IRepository<Post, Guid> _postRepository;

    public GetPostQueryHandeler(
        IRepository<Post, Guid> postRepository)
    {
        _postRepository = postRepository;
    }

    public Task<IResponse<PostDto>> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        var post = _postRepository.Find(request.Id.ToGuid());
        if (post is null)
            return Task.FromResult(Response.Fail<PostDto>( new NullReferenceException()));

        var res = Response.Ok(new PostDto(post));
        Console.WriteLine(res.Data);
        return Task.FromResult(res);
    }
}
