using Core.Ports.Inputs;
using Core.Ports.Repositories;

namespace Adapter.Repositories;

public class MonthRepository : IMonthRepository
{
    private readonly ITimeProvider _timeProvider;

    public MonthRepository(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public string CurrentMonth => _timeProvider.Curent.Month.ToString();

    public IEnumerable<string> AllMonth => ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
}
