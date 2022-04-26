using System.ComponentModel.DataAnnotations;

namespace API.ApiModel;

public class UpdateCurrencyRequest
{
    [Required] public int Id { get; set; }
    public string Name { get; set; }
    public string Sign { get; set; }
    public bool IsActive { get; set; }
    public double Rate { get; set; }
}