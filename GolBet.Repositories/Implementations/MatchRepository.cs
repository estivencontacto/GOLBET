using GolBet.Entities;
using GolBet.Entities.Enums;
using GolBet.Repositories.Data;
using GolBet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GolBet.Repositories.Implementations;

public class MatchRepository(GolBetDbContext context)
    : GenericRepository<Match>(context), IMatchRepository
{
    public async Task<IReadOnlyList<Match>> GetAllWithTeamsAsync(
        MatchStatus? status = null,
        string? search = null,
        CancellationToken cancellationToken = default)
    {
        var query = Context.Matches
            .AsNoTracking()
            .Include(match => match.HomeTeam)
            .Include(match => match.AwayTeam)
            .AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(match => match.Status == status.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var normalizedSearch = search.Trim().ToLower();
            query = query.Where(match =>
                match.HomeTeam.Name.ToLower().Contains(normalizedSearch) ||
                match.AwayTeam.Name.ToLower().Contains(normalizedSearch));
        }

        return await query
            .OrderBy(match => match.MatchDateUtc)
            .ToListAsync(cancellationToken);
    }

    public Task<Match?> GetByIdWithTeamsAsync(int id, CancellationToken cancellationToken = default) =>
        Context.Matches
            .AsNoTracking()
            .Include(match => match.HomeTeam)
            .Include(match => match.AwayTeam)
            .Include(match => match.Bets)
            .FirstOrDefaultAsync(match => match.Id == id, cancellationToken);
}
