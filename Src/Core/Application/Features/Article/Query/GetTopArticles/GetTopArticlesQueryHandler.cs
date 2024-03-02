namespace Application.Features.Article.Query.GetTopArticles;

using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Common.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using ResponseType = IList<KeyValuePair<string, string>>;
public class GetTopArticlesQueryHandler : RequestPipeHandelerBase<GetTopArticelsQuery, ResponseType>
{
    private readonly IArticlesNameRepository _articlesNameRepository;

    public GetTopArticlesQueryHandler(IArticlesNameRepository articlesNameRepository)
    {
        _articlesNameRepository = articlesNameRepository;
    }

    protected override string DefaultErrorMessage => "Can not get top articles";

    protected async override Task<IResponseWrapper<ResponseType>> Execute(GetTopArticelsQuery request)
    {
        var a = _articlesNameRepository.Top(request.n)
            .Select(i => new KeyValuePair<string, string>
                (Base64UrlEncoder.Encode(i.Key.ToByteArray()), i.Value))
            .Select(i => new KeyValuePair<string, string>(
                i.Key == "AAAAAAAAAECAAAAAAAAAAA"
                ? "TermsAndPolicy"
                : i.Key == "EREREREREUGREREREREREQ"
                ? "SupportedProgrammingLanguages"
                : i.Key, i.Value));
        return Ok(a.ToList());
    }
}
