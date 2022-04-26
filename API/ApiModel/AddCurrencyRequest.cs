using System.ComponentModel.DataAnnotations;

namespace API.ApiModel;

public class AddCurrencyRequest
{
    [Required] public string Name { get; set; }
    [Required] public string Sign { get; set; }
    [Required] public bool IsActive { get; set; }
    [Required] public double Rate { get; set; }
}