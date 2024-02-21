using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace NintendoSwitchDeals.Common.Models;

[Index(nameof(NintendoId), IsUnique = true)]
public class Deal
{
    [DisplayName("ID")] public int DealId { get; init; }

    [DisplayName("Nintendo ID")] public long NintendoId { get; init; }

    [DisplayName("Game name")]
    [MaxLength(200)]
    public required string GameName { get; init; }

    [DisplayName("Threshold price")]
    [Precision(6, 2)]
    public decimal ThresholdPrice { get; init; }
    
    [DisplayName("Url")]
    [Url]
    [MaxLength(200)]
    public required string Link { get; init; }
}