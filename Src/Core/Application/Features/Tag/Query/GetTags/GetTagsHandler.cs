using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Common.Repositories;

namespace Application.Features.Tag.Query.GetTags;

using ResponseType = IList<KeyValuePair<string,int>>;

public class GetTagsHandler : RequestPipeHandelerBase<GetTagsQuery, ResponseType>
{
    private ITagRepository _tagRepository;

    public GetTagsHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    protected override string DefaultErrorMessage => "Can not Get Tags";

    protected override async Task<IResponseWrapper<ResponseType>> Execute(GetTagsQuery request)
    {
        var tags = await _tagRepository.GetTop(request.N);
        return Ok(tags); 
    }
}
