using API.ApiModel;
using Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/exchange")]
public class ExchangeController : ControllerBase
{
    private readonly IExchangeService _exchangeService;

    public ExchangeController(IExchangeService exchangeService)
    {
        _exchangeService = exchangeService;
    }

    [HttpGet("/get-highest-currencies")]
    public IActionResult GetHighestCurrencies(int count)
    {
        return Ok(_exchangeService.GetCurrenciesRate(count,true));
    }
    
    [HttpGet("/get-lowest-currencies")]
    public IActionResult GetLowestCurrencies(int count)
    {
        return Ok(_exchangeService.GetCurrenciesRate(count,false));
    }
    
    [HttpPost("/get-most-improved-currencies")]
    public IActionResult GetMostImprovedCurrencies([FromBody] CurrenciesRatesRequest request)
    {
        return Ok(_exchangeService.GetCurrenciesStatus(request.Count,request.StartDate,request.EndDate,true));
    }
    
    [HttpPost("/get-least-improved-currencies")]
    public IActionResult GetLeastImprovedCurrencies([FromBody] CurrenciesRatesRequest request)
    {
        return Ok(_exchangeService.GetCurrenciesStatus(request.Count,request.StartDate,request.EndDate,false));
    }
    [HttpPost("/convert")]
    public IActionResult ConvertCurrency([FromBody] ConversionRequest request)
    {
        double result = _exchangeService.ConvertCurrency(request.Amount, request.CurrentCurrency, request.ToConvertCurrency);
        return Ok(result);
    }
}