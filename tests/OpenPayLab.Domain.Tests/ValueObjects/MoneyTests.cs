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
        Assert.Throws<ArgumentOutOfRangeException>(() => new Money(-100, "eur"));
    }

    [Fact]
    public void Check_ValueObjectsWithTheSameDataAreTheSame()
    {
        // Arrange
        Money first = new Money(100, "EUR");
        Money second = new Money(100, "EUR");

        // Assert
        Assert.Equal(first, second);
    }

    [Fact]
    public void Check_ValueObjectsWithDistinctAmountDataAreDifferents()
    {
        // Arrange
        Money first = new Money(100, "EUR");
        Money second = new Money(200, "EUR");

        // Assert
        Assert.NotEqual(first, second);
    }

    [Fact]
    public void Check_ValueObjectsWithDistinctCurrencyAreDifferents()
    {
        // Arrange
        Money first = new Money(100, "EUR");
        Money second = new Money(100, "USD");

        // Assert
        Assert.NotEqual(first, second);
    }

    [Fact]
    public void Check_TwoHashesAreTheSame()
    {
        // Arrange
        Money first = new Money(100, "EUR");
        Money second = new Money(100, "EUR");

        // Act
        int firstHashCode = first.GetHashCode();
        int secondHashCode = second.GetHashCode();

        // Assert
        Assert.Equal(firstHashCode, secondHashCode);
    }

    [Fact]
    public void Check_TwoEqualMoneyValuesCreateOnlyOneElementInHashSet()
    {
        // Arrange
        HashSet<Money> moneySet = new HashSet<Money>();
        Money first = new Money(100, "EUR");
        Money second = new Money(100, "EUR");

        // Act
        moneySet.Add(first);
        moneySet.Add(second);

        // Assert
        int expected = 1;
        int actual = moneySet.Count;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Check_ObjectsWithTheSameMoneyValueAreEqual()
    {
        // Arrange
        object first = new Money(100, "EUR");
        object second = new Money(100, "EUR");

        // Act & Assert
        Assert.True(first.Equals(second));
    }


    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Check_EmptyCurrencyIsNotAllowed(string? currency)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Money(100, currency!));
    }

    [Theory]
    [InlineData("E")]
    [InlineData("EU")]
    [InlineData("EURO")]
    public void Check_CurrencyMustContainExactlyThreeCharacters(string currency)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Money(100, currency));
    }

    [Fact]
    public void Check_MoneyComparedWithNullReturnsFalse()
    {
        // Arrange
        Money money = new Money(100, "EUR");

        // Act
        bool result = money.Equals((Money?)null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Check_ObjectsOfDifferentTypesAreNotEqual()
    {
        // Arrange
        Money money = new Money(100, "EUR");
        object other = "100 EUR";

        // Act
        bool result = money.Equals(other);

        // Assert
        Assert.False(result);
    }
}
