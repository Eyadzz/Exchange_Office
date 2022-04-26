using Core.Domain.Model;
using Core.Exception;
using Core.Repository;
using Core.Service;
using Domain.Entities;

namespace Service;

public class ExchangeService : IExchangeService
{
    private readonly IExchangeRepository _repository;

    public ExchangeService(IExchangeRepository repository)
    {
        _repository = repository;
    }

    public void Add(ExchangeHistory exchangeHistory)
    {
        _repository.Add(exchangeHistory);
    }

    public List<ExchangeHistory> Get(int id)
    {
        return _repository.Get(id);
    }

    public List<CurrencyRate> GetCurrenciesRate(int count, bool highestRates)
    {
        var result = _repository.GetCurrenciesRate();

        return highestRates
                ? result.OrderByDescending(x => x.Rate).Take(count).ToList()
                : result.OrderBy(x => x.Rate).Take(count).ToList();
    }

    public List<CurrencyRate> GetCurrenciesStatus(int count, DateTime startDate, DateTime endDate, bool mostImproved)
    {
        var result = _repository.GetCurrenciesRateWithinDate(startDate, endDate);

        return mostImproved
            ? result.OrderByDescending(x => x.Rate).Take(count).ToList()
            : result.OrderBy(x => x.Rate).Take(count).ToList();
    }

    public double ConvertCurrency(double amount, string currencyName, string convertToCurrencyName)
    {
        var currentCurrencyHistory = _repository.GetLatestCurrencyHistory(currencyName) ??
                                     throw new CurrencyNotFoundException(currencyName);
        var toConvertCurrencyHistory = _repository.GetLatestCurrencyHistory(convertToCurrencyName) ??
                                     throw new CurrencyNotFoundException(convertToCurrencyName);

        return amount * (currentCurrencyHistory.ExchangeHistory.Rate / toConvertCurrencyHistory.ExchangeHistory.Rate);
    }
}