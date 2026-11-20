using OpenPayLab.Domain.ValueObjects;

namespace OpenPayLab.Domain.Tests.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Create_MoneyValueObject_With100Euros()
    {
        // Arrange
        Money money = new Money(100, "EUR");

        // Act & Assert
        Assert.Equal(100, money.Amount);
        Assert.Equal("EUR", money.Currency);
    }

    [Fact]
    public void Check_CurrencyInLowercase_ReturnsToUppercase()
    {
        // Arrange
        Money money = new Money(100, "eur");

        // Act & Assert
        Assert.Equal("EUR", money.Currency);
    }

    [Fact]
    public void Check_NonNegativeAmountAreNotAllowed()
    {
        // Arrange & Act
        Assert.Throws<ArgumentOutOfRangeException>(() => new Money(-100,"eur"));
    }
}
