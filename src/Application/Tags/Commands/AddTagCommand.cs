using Application.Common.Interfaces;
using Domain.Entity;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tags.Commands;

public class AddTagCommand : IRequest<string>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Colour { get; set; } = string.Empty;
}


public class AddTagCommandHandeler : IRequestHandler<AddTagCommand, string>
{

    public AddTagCommandHandeler()
    {
    }

    public Task<string> Handle(AddTagCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult("");
    }
}
