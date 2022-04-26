using Domain.Entities;

namespace Core.Domain.Model;

public class CurrencyHistory
{
    public string CurrencyName { get; set; }
    public ExchangeHistory ExchangeHistory { get; set; }
}