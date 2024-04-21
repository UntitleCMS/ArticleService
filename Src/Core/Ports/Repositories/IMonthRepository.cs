namespace Core.Ports.Repositories;

public interface IMonthRepository
{
    public string CurrentMonth { get; }
    public IEnumerable<string> AllMonth { get; }
}
