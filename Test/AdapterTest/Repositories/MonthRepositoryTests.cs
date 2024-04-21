using Core.Ports.Inputs;
using Moq;

namespace Adapter.Repositories.Tests;

public class MonthRepositoryTests
{
    [Fact]
    public void MonthRepositoryTest()
    {
        var time = new DateTime(2001,1,19);
        var mock = new Mock<ITimeProvider>();
        mock.Setup(p => p.Curent).Returns(time);
        var expectedAllMonth = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        var expectedMonth = time.Month.ToString();

        var monthRepository = new MonthRepository(mock.Object);
        var actulMonth = monthRepository.CurrentMonth;
        var actulAllMonth = monthRepository.AllMonth;

        Assert.Equal(expectedMonth, actulMonth);
        Assert.Equal(expectedAllMonth, actulAllMonth);
    }
}