using Application.Common.Mediator;

namespace Application.Features.Tag.Query.GetTags;

using ResponsType = IList<KeyValuePair<string,int>>;

public record GetTagsQuery(int N) : IRequestWraped<ResponsType>;
