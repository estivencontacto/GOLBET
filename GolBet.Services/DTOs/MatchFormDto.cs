using System.ComponentModel.DataAnnotations;
using GolBet.Entities.Enums;

namespace GolBet.Services.DTOs;

public class MatchFormDto
{
    public int Id { get; set; }

    [Display(Name = "Equipo local"), Range(1, int.MaxValue, ErrorMessage = "Seleccione el equipo local.")]
    public int HomeTeamId { get; set; }

    [Display(Name = "Equipo visitante"), Range(1, int.MaxValue, ErrorMessage = "Seleccione el equipo visitante.")]
    public int AwayTeamId { get; set; }

    [Display(Name = "Fecha y hora"), Required]
    public DateTime MatchDate { get; set; }

    [Display(Name = "Estadio"), Required, StringLength(150)]
    public string Venue { get; set; } = string.Empty;

    [Display(Name = "Jornada"), Range(1, 50)]
    public int Matchday { get; set; } = 1;

    [Display(Name = "Estado")]
    public MatchStatus Status { get; set; } = MatchStatus.Scheduled;

    [Display(Name = "Marcador local"), Range(0, 99)]
    public int? HomeScore { get; set; }

    [Display(Name = "Marcador visitante"), Range(0, 99)]
    public int? AwayScore { get; set; }

    [Display(Name = "Cuota local"), Range(typeof(decimal), "1.01", "999.99")]
    public decimal HomeOdds { get; set; } = 1.80m;

    [Display(Name = "Cuota empate"), Range(typeof(decimal), "1.01", "999.99")]
    public decimal DrawOdds { get; set; } = 3.00m;

    [Display(Name = "Cuota visitante"), Range(typeof(decimal), "1.01", "999.99")]
    public decimal AwayOdds { get; set; } = 3.80m;
}
