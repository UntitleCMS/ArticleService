using Application.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tag.Query.SearchTags;

using ResponsType = IList<KeyValuePair<string, int>>;

public record SerchTagsQuery( string Tag_like, int MaxResult = 10 ) : IRequestWraped<ResponsType>;
