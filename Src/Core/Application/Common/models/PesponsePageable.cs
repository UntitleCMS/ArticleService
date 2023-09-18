
using Application.Common.Interfaces;

namespace Application.Common.models;

public class Pageable<T> : IResponsePageable<T>
{
    public IEnumerable<T> Collections { get; set; } = Enumerable.Empty<T>();
    public virtual string Pivot { get; set; } = string.Empty;
    public virtual bool HasNext { get; set; }
    public virtual bool HasPrevious { get; set; }
    public int CountAll { get; set; }

    public int CountData => Collections.Count();

}
