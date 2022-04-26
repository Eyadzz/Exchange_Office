using System.ComponentModel.DataAnnotations;

namespace API.ApiModel;

public class ConversionRequest
{
    [Required] public double Amount { get; set; }
    [Required] public string CurrentCurrency { get; set; }
    [Required] public string ToConvertCurrency { get; set; }
}