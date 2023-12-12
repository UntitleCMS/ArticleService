using Application.Common.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Collections;
using MongoDB.Bson;
using MongoDB.Driver;

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
}
