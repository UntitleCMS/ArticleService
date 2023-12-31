using Application.Common.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Collections;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Text.RegularExpressions;

namespace Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private DataContext _mongo;
    private IMongoCollection<PostCollection> _posts;

    public TagRepository(DataContext mongo)
    {
        _mongo = mongo;
        _posts = _mongo.Collection<PostCollection>();
    }

    public async Task<IList<KeyValuePair<string, int>>> GetTop(int n = 10)
    {
        var x = _posts.AsQueryable()
            .SelectMany(i => i.Tags)
            .GroupBy(i => i)
            .Select(i => new KeyValuePair<string, int>(i.Key, i.Count()))
            .OrderByDescending(i => i.Value)
            .Take(n);

        Console.WriteLine(n);
        Console.WriteLine(x.ToJson(new() { Indent = true }));

        return x.ToList();
    }

    public async Task<IList<string>> Serch(string tag, int n = 10)
    {
        var x = _posts.Aggregate()
            .Unwind(i=>i.Tags)
            .Group(new BsonDocument("_id", "$Tags"))
            .Match(new BsonDocument("_id", new Regex(tag, RegexOptions.IgnoreCase)))
            .Limit(n)
            .ToList()
            .Select(i => i["_id"].ToString());

        Console.WriteLine(n);
        Console.WriteLine(x.Count());
        Console.WriteLine(x.ToJson(new() { Indent = true }));

        return x.ToList();
    }
}
