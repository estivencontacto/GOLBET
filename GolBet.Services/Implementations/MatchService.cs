using AutoMapper;
using GolBet.Entities.Enums;
using GolBet.Repositories.Interfaces;
using GolBet.Services.DTOs;
using GolBet.Services.Interfaces;

namespace GolBet.Services.Implementations;

public class MatchService(IMatchRepository matchRepository, IMapper mapper) : IMatchService
{
    private static readonly TimeZoneInfo ColombiaTimeZone = ResolveColombiaTimeZone();

    public async Task<IReadOnlyList<MatchDto>> GetMatchesAsync(
        MatchStatus? status = null,
        CancellationToken cancellationToken = default)
    {
        var matches = await matchRepository.GetAllWithTeamsAsync(status, cancellationToken);
        return matches.Select(ToDto).ToList();
    }

    public async Task<MatchDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var match = await matchRepository.GetByIdWithTeamsAsync(id, cancellationToken);
        return match is null ? null : ToDto(match);
    }

    private MatchDto ToDto(Entities.Match match)
    {
        var dto = mapper.Map<MatchDto>(match);
        dto.MatchDate = TimeZoneInfo.ConvertTimeFromUtc(
            DateTime.SpecifyKind(match.MatchDateUtc, DateTimeKind.Utc),
            ColombiaTimeZone);
        dto.StatusName = match.Status switch
        {
            MatchStatus.Scheduled => "Programado",
            MatchStatus.Live => "En vivo",
            MatchStatus.Finished => "Finalizado",
            _ => "Sin estado"
        };
        return dto;
    }

    private static TimeZoneInfo ResolveColombiaTimeZone()
    {
        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
        }
        catch (TimeZoneNotFoundException)
        {
            return TimeZoneInfo.FindSystemTimeZoneById("America/Bogota");
        }
    }
}
