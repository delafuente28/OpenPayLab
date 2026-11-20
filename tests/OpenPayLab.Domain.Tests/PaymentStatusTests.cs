
namespace OpenPayLab.Domain.Tests;

public class PaymentStatusTests
{
    [Fact]
    public void PaymentStatus_ShouldContainExpectedValues()
    {
        string[] expectedStatuses = new[]
        {
            "Pending",
            "Authorized",
            "Captured",
            "Failed",
            "Cancelled"
        };

        string[] actualStatuses = Enum.GetNames<OpenPayLab.Domain.Enums.PaymentStatus>();

        Assert.Equal(expectedStatuses, actualStatuses);
    }
}
