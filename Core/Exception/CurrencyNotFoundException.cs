namespace Core.Exception;

public class CurrencyNotFoundException : NotFoundException
{
    public CurrencyNotFoundException(string currencyName)
        : base($"The specified currency name does not exist: " + currencyName) {}
}