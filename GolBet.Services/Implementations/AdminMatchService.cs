using GolBet.Entities;
using GolBet.Repositories.Interfaces;
using GolBet.Services.DTOs;
using GolBet.Services.Interfaces;

namespace GolBet.Services.Implementations;

public class AdminMatchService(
    IMatchRepository matchRepository,
    IGenericRepository<Team> teamRepository) : IAdminMatchService
{
    private static readonly TimeZoneInfo ColombiaTimeZone = ResolveColombiaTimeZone();

    public async Task<IReadOnlyList<TeamOptionDto>> GetTeamsAsync(CancellationToken cancellationToken = default) =>
        (await teamRepository.GetAllAsync(cancellationToken))
            .OrderBy(team => team.Name)
            .Select(team => new TeamOptionDto { Id = team.Id, Name = team.Name })
            .ToList();

    public async Task<MatchFormDto?> GetForEditAsync(int id, CancellationToken cancellationToken = default)
    {
        var match = await matchRepository.GetByIdAsync(id, cancellationToken);
        return match is null ? null : new MatchFormDto
        {
            Id = match.Id,
            HomeTeamId = match.HomeTeamId,
            AwayTeamId = match.AwayTeamId,
            MatchDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(match.MatchDateUtc, DateTimeKind.Utc), ColombiaTimeZone),
            Venue = match.Venue,
            Matchday = match.Matchday,
            Status = match.Status,
            HomeScore = match.HomeScore,
            AwayScore = match.AwayScore,
            HomeOdds = match.HomeOdds,
            DrawOdds = match.DrawOdds,
            AwayOdds = match.AwayOdds
        };
    }

    public async Task CreateAsync(MatchFormDto model, CancellationToken cancellationToken = default)
    {
        var match = new Match();
        CopyValues(model, match);
        await matchRepository.AddAsync(match, cancellationToken);
        await matchRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(MatchFormDto model, CancellationToken cancellationToken = default)
    {
        var match = await matchRepository.GetByIdAsync(model.Id, cancellationToken);
        if (match is null) return false;

        CopyValues(model, match);
        matchRepository.Update(match);
        await matchRepository.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var match = await matchRepository.GetByIdAsync(id, cancellationToken);
        if (match is null) return false;

        matchRepository.Remove(match);
        await matchRepository.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static void CopyValues(MatchFormDto source, Match target)
    {
        target.HomeTeamId = source.HomeTeamId;
        target.AwayTeamId = source.AwayTeamId;
        target.MatchDateUtc = TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(source.MatchDate, DateTimeKind.Unspecified), ColombiaTimeZone);
        target.Venue = source.Venue.Trim();
        target.Matchday = source.Matchday;
        target.Status = source.Status;
        target.HomeScore = source.HomeScore;
        target.AwayScore = source.AwayScore;
        target.HomeOdds = source.HomeOdds;
        target.DrawOdds = source.DrawOdds;
        target.AwayOdds = source.AwayOdds;
    }

    private static TimeZoneInfo ResolveColombiaTimeZone()
    {
        try { return TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"); }
        catch (TimeZoneNotFoundException) { return TimeZoneInfo.FindSystemTimeZoneById("America/Bogota"); }
    }
}
