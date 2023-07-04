using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Query;

public class GetAllPostsQuery : IRequest<string> { }

public class GetAllPostsQueryHandeler : IRequestHandler<GetAllPostsQuery, string>
{
    public Task<string> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult("Hello");
    }
}
