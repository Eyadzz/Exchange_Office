using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities;

public class ExchangeHistory : BaseEntity
{
    [Required] public int CurrencyId { get; set; }
    [Required] public DateTime ExchangeDate { get; set; }
    [Required] public double Rate { get; set; }
    
}