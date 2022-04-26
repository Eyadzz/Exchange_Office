using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities;

public class Currency : BaseEntity
{
    [Required] public string Name { get; set; }
    [Required] public string Sign { get; set; }
    [Required] public bool IsActive { get; set; }
}