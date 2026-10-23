using GolBet.Entities.Enums;
using GolBet.Services.DTOs;

namespace GolBet.Services.Interfaces;

public interface IMatchService
{
    Task<IReadOnlyList<MatchDto>> GetMatchesAsync(
        MatchStatus? status = null,
        string? search = null,
        CancellationToken cancellationToken = default);

    Task<MatchDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
