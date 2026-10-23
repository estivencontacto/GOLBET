using GolBet.Services.DTOs;

namespace GolBet.Services.Interfaces;

public interface IAdminMatchService
{
    Task<IReadOnlyList<TeamOptionDto>> GetTeamsAsync(CancellationToken cancellationToken = default);
    Task<MatchFormDto?> GetForEditAsync(int id, CancellationToken cancellationToken = default);
    Task CreateAsync(MatchFormDto model, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(MatchFormDto model, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
