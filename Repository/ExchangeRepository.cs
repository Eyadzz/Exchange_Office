using Core.Domain.Model;
using Core.Repository;
using Domain.Entities;

namespace Repository;

public class ExchangeRepository : IExchangeRepository
{
    private readonly ApplicationDbContext _context;

    public ExchangeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public void Add(ExchangeHistory exchangeHistory)
    {
        _context.ExchangeHistories.Add(exchangeHistory);
        _context.SaveChanges();
    }

    public List<ExchangeHistory> Get(int id)
    {
        return _context.ExchangeHistories.Where(h => h.CurrencyId == id).ToList();
    }

    public IEnumerable<CurrencyRate> GetCurrenciesRate()
    {
        return _context.ExchangeHistories
            .Join(_context.Currencies, exchange => exchange.CurrencyId, currency => currency.Id,
                (exchange, currency) => new {currencyName = currency.Name, history = exchange})
            .GroupBy(g => new {g.currencyName})
            .Select(group => new CurrencyRate
                {Name = group.Key.currencyName, Rate = group.OrderByDescending(arg => arg.history.ExchangeDate).FirstOrDefault()!.history.Rate});
    }

    public IEnumerable<CurrencyRate> GetCurrenciesRateWithinDate(DateTime startDate, DateTime endDate)
    {
        var queryResult = _context.ExchangeHistories
            .Join(_context.Currencies, exchange => exchange.CurrencyId, currency => currency.Id,
                (exchange, currency) => new {currencyName = currency.Name, history = exchange})
            .Where(arg => arg.history.ExchangeDate >= startDate)
            .Where(arg => arg.history.ExchangeDate <= endDate)
            .GroupBy(g => new {g.currencyName})
            .Select(g => new {CurrencyName = g.Key.currencyName, HisoryList = g.ToList()});

        List<CurrencyRate> ratesResponse = new List<CurrencyRate>();
        foreach (var rateHistory in queryResult)
        {
            double sum = 0;
            double previousRate = rateHistory.HisoryList.First().history.Rate;
            
            for (int i = 1; i < rateHistory.HisoryList.Count; i++)
            {
                sum += previousRate - rateHistory.HisoryList[i].history.Rate ;
                previousRate = rateHistory.HisoryList[i].history.Rate;
            }

            ratesResponse.Add(new CurrencyRate{Name = rateHistory.CurrencyName, Rate = sum});
        }

        return ratesResponse;
    }

    public CurrencyHistory GetLatestCurrencyHistory(string name)
    {
        var queryResult = _context.ExchangeHistories
            .Join(_context.Currencies, exchange => exchange.CurrencyId, currency => currency.Id,
                (exchange, currency) => new CurrencyHistory{CurrencyName = currency.Name, ExchangeHistory = exchange})
            .OrderByDescending(arg => arg.ExchangeHistory.ExchangeDate)
            .FirstOrDefault(arg => arg.CurrencyName == name);

        return queryResult!;
    }
}