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
