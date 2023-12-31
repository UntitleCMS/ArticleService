using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Common.Repositories;
using System.Threading.Tasks;

namespace Application.Features.Tag.Query.SearchTags;
using ResponseType = IList<KeyValuePair<string,int>>;

public class SerchTagsQueryHandler : RequestPipeHandelerBase<SerchTagsQuery, ResponseType>
{
    private ITagRepository _tagRepository;

    public SerchTagsQueryHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    protected override string DefaultErrorMessage => "Can not serch tag";

    protected override async Task<IResponseWrapper<ResponseType>> Execute(SerchTagsQuery request)
    {
        var tags = await _tagRepository.Serch(request.Tag_like, request.MaxResult);
        return Ok(tags.Select(i=>new KeyValuePair<string, int>(i,-1)).ToList());
    }
}
