using Core.Ports.Inputs;

namespace Adapter.Inputs;

public class HostTimeProvider : ITimeProvider
{
    public DateTime Curent => DateTime.Now;
}
