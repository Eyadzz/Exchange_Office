using Domain.Entities;

namespace API.ApiModel;

public class GetCurrencyResponse
{
    public Currency Currency { get; set; }
    public List<ExchangeHistory> ExchangeHistory { get; set; }
}