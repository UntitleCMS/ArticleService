namespace Application.Common.Models;

public class PageableWrapper<T>
{
    public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }
    public int CountAll { get; set; }
    public int CountData { get => Data.Count(); }
}
