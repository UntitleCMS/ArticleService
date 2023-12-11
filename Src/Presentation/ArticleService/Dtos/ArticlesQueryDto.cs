using Application.Features.Article.Query.GetArticels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ArticleService.Dtos;

public class ArticlesQueryDto
{

    [FromQuery(Name = "sub")]
    public string? Sub { get; set; }

    [FromQuery(Name = "take")]
    public int Take { set; get; }

    [FromQuery(Name ="from")]
    [RegularExpression("""^[<>][A-Za-z0-9_\-\+\/]{22,24}$""", ErrorMessage = "Start with '<' for get befor, '>' for after; example '<C95KV0xhcUuD1s5HHWM4uA' or '>C95KV0xhcUuD1s5HHWM4uA'")]
    public string? Privot { set; get; }

    [FromQuery(Name ="of")]
    [RegularExpression("""^[A-Za-z0-9_\-\+\/]{22,24}$""", ErrorMessage ="For base64 id")]
    public string? Filter { get; set; }

    [RegularExpression("""[^\s\n\t]+""", ErrorMessage ="Tag sould not be space")]
    public string[]? Tags { get; set; }
}
