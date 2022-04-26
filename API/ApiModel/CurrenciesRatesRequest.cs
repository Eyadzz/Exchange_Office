using System.ComponentModel.DataAnnotations;

namespace API.ApiModel;

public class CurrenciesRatesRequest
{
    [Required] public int Count { get; set; }
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
}