namespace Application.Features.Article.Query.GetTopArticles;

using Application.Common.Mediator;
using ResponseType = IList<KeyValuePair<string,string>>;
public record GetTopArticelsQuery(int n) : IRequestWraped<ResponseType>;
