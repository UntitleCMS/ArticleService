using Application.Common.Extentions;
using Application.Common.Interfaces;
using Domain.Entity;
using MediatR;
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
    public DeletePostCommandHandler()
    {
    }

    public Task<string> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult("");
    }
}
