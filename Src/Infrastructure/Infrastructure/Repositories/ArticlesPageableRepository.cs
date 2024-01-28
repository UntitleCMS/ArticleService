using Application.Common.Interfaces;
using Application.Common.models;
using Application.Common.Repositories;
using Domain.Entites;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Collections;
using Infrastructure.Queues;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Repositories;

class Tmp
{
    public string authorId { get; set; } = string.Empty;
    public string articleId { get; set; } = string.Empty;
    public DateTime? cratedAt { get; set; }
}

public class ArticlesPageableRepository : IArticlesPageableRepository
{
    private DataContext _context;
    private FollowingArticlesRequest _followingArticlesRequest;

    public ArticlesPageableRepository(DataContext context, FollowingArticlesRequest followingArticlesRequest)
    {
        _context = context;
        _followingArticlesRequest = followingArticlesRequest;
    }

    public async Task<IResponsePageable<PostEntity>> Find(ArticleFilter fillter, string? Sub = null)
    {
        Console.WriteLine($"filter >> {fillter.ToJson(new() { Indent = true })}");

        var data = new Pageable<PostEntity>();


        var resx = (Sub is not null && fillter.OnlyFollowing)
            ? GetPostEntityQueryableOnlyFollowing(fillter, Sub)
            : GetPostEntityQueryable(Sub);


        var datasize = ApplyFilter(ref resx, fillter, Sub);
        //Console.WriteLine(resx.ToArray().ToJson(new() { Indent = true })) ;
        ApplySerch(ref resx, fillter);
        (bool pre, bool post) = ApplyPrivotFilter(ref resx, fillter);

        data.HasPrevious = pre;
        data.HasNext = post;
        data.Collections = resx.Take(fillter.Take);
        data.Pivot = fillter.Before ?? fillter.After ?? string.Empty;
        data.CountAll = datasize;

        //Console.WriteLine($">> {data.ToJson(new() { Indent = true })}");

        return data;
    }

    private IQueryable<PostEntity> GetPostEntityQueryableOnlyFollowing(ArticleFilter filter, string? Sub = default)
    {
        var aa = _followingArticlesRequest.PublishAsync(new
        {
            Sub,
            filter.Take,
            filter.Before,
            filter.After,
        });

        aa.Wait();

        if (aa.IsFaulted)
            throw aa.Exception!;

        var ob = JsonSerializer.Deserialize<Tmp[]>(aa.Result)
            ?.Select(i=>new Guid(Base64UrlEncoder.DecodeBytes(i.articleId)))
            .ToArray();

        Console.WriteLine(ob.ToJson(new() { Indent=true })) ;

        var res = _context.Collection<PostCollection>()
                   .AsQueryable()
                   .Where(i=>ob.Contains(i.ID))
                   .OrderByDescending(i => i.Timestamp)
                   .Select(article => new PostEntity()
                   {
                       ID = article.ID,
                       AuthorId = article.AuthorId,

                       Title = article.Title,
                       Cover = article.Cover,
                       Content = article.Content,
                       ContentPreviews = article.ContentPreviews,
                       Tags = article.Tags,

                       IsPublished = article.IsPublished,
                       CreatedAt = article.Timestamp,
                       LastUpdated = article.LastUpdate,

                       SavedBy = article.SavedBy.Where(i => i == Sub).ToList(),
                       LikedBy = article.LikedBy.Where(i => i == Sub).ToList(),
                       LikedCount = article.LikedBy.Count()
                   });


        return res;

    }
    private IQueryable<PostEntity> GetPostEntityQueryable(string? Sub = default)
    {
        var res = _context.Collection<PostCollection>()
           .AsQueryable()
           .OrderByDescending(i => i.Timestamp)
           .Select(article => new PostEntity()
           {
               ID = article.ID,
               AuthorId = article.AuthorId,

               Title = article.Title,
               Cover = article.Cover,
               Content = article.Content,
               ContentPreviews = article.ContentPreviews,
               Tags = article.Tags,

               IsPublished = article.IsPublished,
               CreatedAt = article.Timestamp,
               LastUpdated = article.LastUpdate,

               SavedBy = article.SavedBy.Where(i => i == Sub).ToList(),
               LikedBy = article.LikedBy.Where(i => i == Sub).ToList(),
               LikedCount = article.LikedBy.Count()
           });

        return res;
    }

    private void ApplySerch(ref IQueryable<PostEntity> q, in ArticleFilter fillter)
    {
        if (!fillter.SerchText.IsNullOrEmpty())
        {
            var txt = fillter.SerchText!;
            q = q.Where( i => i.Title.ToUpper().Contains(txt.ToUpper()));
        }
    }
    private int ApplyFilter(ref IQueryable<PostEntity> q, in ArticleFilter fillter, string? sub = default)
    {
        if (fillter.Of is not null)
        {
            var uid = fillter.Of;
            q = q.Where(i => uid == null || i.AuthorId == uid);
        }

        if (fillter.Tags is not null)
        {
            var tags = fillter.Tags;
            q = q.Where(i => tags == null || i.Tags.Any(x => tags.Contains(x)));
        }

        if (fillter.IsBookmarked)
        {
            q = q.Where(i => sub != null && i.SavedBy.Contains(sub));
        }

        if (fillter.IsPublish)
        {
            q = q.Where(i =>  i.IsPublished == true);
        }
        else
        {
            q = q.Where(i => i.IsPublished == false && i.AuthorId == sub);
        }


        return q.Count();
    }

    /**
     * returns (is have previous, is have post)
     */
    private (bool, bool) ApplyPrivotFilter(ref IQueryable<PostEntity> q, in ArticleFilter fillter)
    {
        if (fillter.Before is not null && fillter.After is not null)
        {
            throw new Exception("Befor and After should not have value in the same time.");
        }

        bool pre = false;
        bool post = false;
        bool back = false;

        var privotID = fillter.Before ?? fillter.After;

        Guid? privotGUID = privotID is not null
            ? new Guid(Base64UrlEncoder.DecodeBytes(privotID))
            : null;

        var privot = _context.Collection<PostCollection>()
            .Find(i => i.ID == privotGUID)
            .FirstOrDefault();

        if (fillter.Before is not null)
        {
            back = q.Where(i => i.CreatedAt > privot.Timestamp).Take(1).Count() == 1;
            q = q.Where(i => i.CreatedAt < privot.Timestamp);
        }

        else if (fillter.After is not null)
        {
            q = q.OrderBy(i => i.CreatedAt);
            back = q.Where(i => i.CreatedAt < privot.Timestamp).Take(1).Count() == 1;
            q = q.Where(i => i.CreatedAt > privot.Timestamp);
        }

        q = q.Take(fillter.Take + 1);

        var next = q.Count() > fillter.Take;

        pre = fillter.Before is not null && next;
        pre = pre || (fillter.After is not null && back);

        post = fillter.After is not null && next;
        post = post || (fillter.Before is not null && back);

        return (pre, post);
    }

}
