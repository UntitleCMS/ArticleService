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
    private readonly IAppMongoDbContext _db;

    public AddTagCommandHandeler(IAppMongoDbContext db)
    {
        _db = db;
    }

    public Task<string> Handle(AddTagCommand request, CancellationToken cancellationToken)
    {
        var tag = _db.Tags.Find(request.Name);
        if (tag is not null)
            return Task.FromResult("tag is exist.");
        var t = new Tag()
        {
            ID = request.Name,
            TagColour = request.Colour,
        };
        _db.Tags.Add(t);
        _db.SaveChanges();

        return Task.FromResult(JsonConvert.SerializeObject(t,Formatting.Indented));
    }
}
