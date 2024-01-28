using Application.Common.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Collections;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class ArticlesNameRepository : IArticlesNameRepository
{
    private DataContext _context;

    public ArticlesNameRepository(DataContext context)
    {
        _context = context;
    }

    public IList<KeyValuePair<Guid, string>> Top(int n = 10)
    {
        var posts = _context.Collection<PostCollection>();
        var res = posts.Aggregate()
            .Match(i=>i.IsPublished == true)
            .Project(i => new
            {
                ID = i.ID,
                Title = i.Title,
                LikeCount = i.LikedBy.Count
            })
            .SortByDescending(i => i.LikeCount)
            .Limit(n).ToList();
        return res.Select(i=>new KeyValuePair<Guid,string>(i.ID,i.Title)).ToList();
    }
}
