using MediatR;
using Application.Common.Interfaces;
using System.Text.Json.Serialization;
using Domain.Entites;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Article.Command.UpdateArticle;

using CommandType = IRequest<IResponseWrapper<string>>;

public class UpdateArticleCommand : CommandType
{
    [Required(ErrorMessage = "Post title should not be null")]
    public virtual string Title { get; set; } = string.Empty;
    public virtual string Description { get; set; } = string.Empty;
    public virtual string CoverImage { get; set; } = string.Empty;
    public virtual string Content { get; set; } = string.Empty;
    [MaxLength(5)]
    public virtual string[] Tags { get; set; } = Array.Empty<string>();
    public virtual bool IsPublish { get; set; } = false;

    [JsonIgnore]
    public virtual string Sub { get; set; } = string.Empty;
    [JsonIgnore]
    public virtual string ID { get; set; } = string.Empty;

    [JsonIgnore]
    public PostEntity PostEntity =>
        new()
        {
            Title = Title,
            AuthorId = Sub,
            ID = ID.IsNullOrEmpty() ? default : new Guid(Base64UrlEncoder.DecodeBytes(ID)),
            Content = Content,
            Tags = Tags,
            ContentPreviews = Description,
            Cover = CoverImage,
            IsPublished = IsPublish
        };

    public ValidationException? Validate()
    {
        try { _ = new Guid(Base64UrlEncoder.DecodeBytes(ID)); }
        catch (Exception) { return new ValidationException("Invalid ID"); }

        ValidationContext context = new(this, serviceProvider: null, items: null);
        List<ValidationResult> results = new();
        bool isValid = Validator.TryValidateObject(this, context, results, true);

        if (isValid == false)
        {
            return new ValidationException(results.First().ErrorMessage);
        }
        return null;
    }
}
