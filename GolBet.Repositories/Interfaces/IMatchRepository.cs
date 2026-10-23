using GolBet.Entities;
using GolBet.Entities.Enums;

namespace GolBet.Repositories.Interfaces;

public interface IMatchRepository : IGenericRepository<Match>
{
    Task<IReadOnlyList<Match>> GetAllWithTeamsAsync(
        MatchStatus? status = null,
        string? search = null,
        CancellationToken cancellationToken = default);

    Task<Match?> GetByIdWithTeamsAsync(int id, CancellationToken cancellationToken = default);
}
