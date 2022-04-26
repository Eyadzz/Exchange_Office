using API.ApiModel;
using Core.Service;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Route("api/currency")]
public class CurrencyController : ControllerBase
{
    private readonly IExchangeService _exchangeService;
    private readonly ICurrencyService _currencyService;

    public CurrencyController(ICurrencyService currencyService, IExchangeService exchangeService)
    {
        _currencyService = currencyService;
        _exchangeService = exchangeService;
    }
    
    [HttpPost("/add")]
    public IActionResult Add([FromBody] AddCurrencyRequest request)
    {
        Currency newCurrency = new Currency
        {
            Name = request.Name,
            Sign = request.Sign,
            IsActive = request.IsActive
        };
        _currencyService.Add(newCurrency);
        ExchangeHistory exchangeHistory = new ExchangeHistory
        {
            CurrencyId = newCurrency.Id,
            ExchangeDate = DateTime.Now.Date,
            Rate = request.Rate
        };
        _exchangeService.Add(exchangeHistory);
        return Ok(newCurrency);
    }
    
    [HttpPut("/update")]
    public IActionResult Update([FromBody] UpdateCurrencyRequest request)
    {
        Currency newCurrency = new Currency
        {
            Id = request.Id,
            Name = request.Name,
            Sign = request.Sign,
            IsActive = request.IsActive
        };
        _currencyService.Update(newCurrency);
        ExchangeHistory exchangeHistory = new ExchangeHistory
        {
            CurrencyId = request.Id,
            ExchangeDate = DateTime.Now.Date,
            Rate = request.Rate
        };
        _exchangeService.Add(exchangeHistory);
        
        return Ok("Currency with ID " + newCurrency.Id + " was updated successfully");
    }
    
    [HttpDelete("/delete")]
    public IActionResult Delete(string name)
    {
        _currencyService.Delete(name);
        return Ok("Currency deleted successfully");
    }
    
    [HttpGet("/get")]
    public IActionResult Get(string name)
    {
        var currency = _currencyService.Get(name);
        
        if(currency == null)
            return NotFound("Cannot find currency with name: " + name);

        var currencyHistory = _exchangeService.Get(currency.Id);

        GetCurrencyResponse response = new GetCurrencyResponse
        {
            Currency = currency,
            ExchangeHistory = currencyHistory
        };

        return Ok(response);
    }
    
    [HttpGet("/getall")]
    public IActionResult GetAll()
    {
        var currencies = _currencyService.GetAll();
        List<GetCurrencyResponse> response = new List<GetCurrencyResponse>();
        foreach (var currency in currencies)
        {
            var currencyHistory = _exchangeService.Get(currency.Id);
            GetCurrencyResponse getCurrencyResponse = new GetCurrencyResponse
            {
                Currency = currency,
                ExchangeHistory = currencyHistory
            };
            response.Add(getCurrencyResponse);
        }
        return Ok(response);
    }
}