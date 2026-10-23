using System.ComponentModel.DataAnnotations;
using GolBet.Entities.Common;
using GolBet.Entities.Enums;

namespace GolBet.Entities;

public class Bet : AuditableEntity
{
    public int MatchId { get; set; }
    public Match Match { get; set; } = null!;

    [Required, MaxLength(100)]
    public string BettorName { get; set; } = string.Empty;

    public BetOutcome Outcome { get; set; }
    public decimal Amount { get; set; }
    public decimal Odds { get; set; }
}
