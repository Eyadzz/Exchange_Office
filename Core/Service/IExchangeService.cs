using Core.Domain.Model;
using Domain.Entities;

namespace Core.Service;

public interface IExchangeService
{
    void Add(ExchangeHistory exchangeHistory);
    List<ExchangeHistory> Get(int id);
    List<CurrencyRate> GetCurrenciesRate(int count, bool highestRates);
    List<CurrencyRate> GetCurrenciesStatus(int count, DateTime startDate, DateTime endDate, bool mostImproved);
    double ConvertCurrency(double amount, string currencyName, string convertToCurrencyName);
}