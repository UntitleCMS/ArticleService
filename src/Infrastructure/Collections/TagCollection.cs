
using Infrastructure.Common.Models;

namespace Infrastructure.Collections;

public class TagCollection : BaseCollection<string>
{
    public string Colout { get; set; } = string.Empty;
    public string UnicodeIcon { get; set; } = string.Empty;
}
