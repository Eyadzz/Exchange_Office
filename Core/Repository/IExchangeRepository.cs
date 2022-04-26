using Core.Domain.Model;
using Domain.Entities;

namespace Core.Repository;

public interface IExchangeRepository
{
    void Add(ExchangeHistory exchangeHistory);
    List<ExchangeHistory> Get(int id);
    IEnumerable<CurrencyRate> GetCurrenciesRate();
    IEnumerable<CurrencyRate> GetCurrenciesRateWithinDate(DateTime startDate, DateTime endDate);
    CurrencyHistory GetLatestCurrencyHistory(string name);
}