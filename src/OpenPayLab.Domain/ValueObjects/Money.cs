namespace OpenPayLab.Domain.ValueObjects;

public sealed class Money
{
    public decimal Amount {get;}

    public string Currency {get;}

    public Money(decimal amount, string currency)
    {
        ValidateNonNegativeAmount(amount);
        Amount = amount;

        ValidateCurrency(currency);
        Currency = currency.ToUpperInvariant();
    }

    private void ValidateNonNegativeAmount(decimal amount)
    {
        if(amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }
    }

    private void ValidateCurrency(string currency)
    {
        if(string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency cannot be empty.", nameof(currency));
        }

        if(currency.Length != 3)
        {
            throw new ArgumentException("Currency must contain exactly three characters.", nameof(currency));
        }
    }
}
