namespace Adapter.Inputs.Tests;

public class HostTimeProviderTests
{
    [Fact()]
    public void Current()
    {
        HostTimeProvider timeProvider = new ();

        DateTime? now = timeProvider.Curent;

        Assert.NotNull(now);
    }
}