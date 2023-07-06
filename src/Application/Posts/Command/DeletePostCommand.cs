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

namespace Application.Posts.Command;

public class DeletePostCommand : IRequest<string>
{
    public Guid PostId { get; set; }
    public string UserId { get; set; } = string.Empty;
}

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, string>
{
    private readonly IAppDbContext _appDbContext;

    public DeletePostCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public Task<string> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var a = _appDbContext.Posts
            .Where(p => p.ID == request.PostId)
            .Where(p => p.OwnerID == request.UserId)
            .ExecuteDelete();


        return Task.FromResult(a.ToString());

    }
}
