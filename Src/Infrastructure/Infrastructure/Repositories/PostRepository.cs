using Application.Common.Repositories;
using Domain.Entites;
using Domain.Exceptions;
using Infrastructure.Persistence.Collections;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly ILogger<PostRepository> _logger;
    private readonly IMongoClient _mongo;
    private readonly IMongoDatabase _db;
    private readonly IMongoCollection<PostCollection> _collection;
    public PostRepository(IMongoClient mongo, ILogger<PostRepository> logger)
    {
        _mongo = mongo;
        _db = _mongo.GetDatabase("article");
        _collection = _db.GetCollection<PostCollection>("posts");
        _logger = logger;
    }

    public Task<PostEntity> FindById(string id, string? sub = null, CancellationToken cancellationToken = default)
    {
        try
        {
            Guid Id;
            Id = new Guid(Base64UrlEncoder.DecodeBytes(id));
            var result = _collection.AsQueryable()
                .Where(i=>i.ID == Id)
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

                    SavedBy = article.SavedBy.Where(i => i == sub).ToArray(),
                    LikedBy = article.LikedBy.Where(i => i == sub).ToArray()
                }).FirstOrDefault();


            if (result is null)
                return Task.FromException<PostEntity>(new ArticleNotFoundException());

            _logger.LogInformation("Find post[{postID}] with sub[{sub}] : {result}", id, sub, result.ToJson(new() { Indent = true}));

            return Task.FromResult(result);

        }
        catch (Exception ex)
        {
            return Task.FromException<PostEntity>(ex);
        }

    }

    public Task SavePost(ref PostEntity post, CancellationToken cancellationToken = default)
    {
        try
        {
            var newPost = new PostCollection()
            {
                Content = post.Content,
                ContentPreviews = post.ContentPreviews,
                Cover = post.Cover,
                IsPublished = post.IsPublished,
                Tags = post.Tags,
                Title = post.Title,
                AuthorId = post.AuthorId,
                LastUpdate = DateTime.UtcNow,
                Timestamp = DateTime.UtcNow
            };

            _collection
                .InsertOneAsync(newPost, cancellationToken: cancellationToken)
                .Wait(cancellationToken);

            post.ID = newPost.ID;

        }
        catch (Exception ex)
        {
            return Task.FromException(ex);
        }

        return Task.CompletedTask;
    }
}
