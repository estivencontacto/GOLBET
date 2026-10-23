using GolBet.Entities.Common;
using GolBet.Repositories.Data;
using GolBet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GolBet.Repositories.Implementations;

public class GenericRepository<T>(GolBetDbContext context) : IGenericRepository<T>
    where T : AuditableEntity
{
    protected readonly GolBetDbContext Context = context;
    protected DbSet<T> Entities => Context.Set<T>();

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await Entities.AsNoTracking().ToListAsync(cancellationToken);

    public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        Entities.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

    public Task AddAsync(T entity, CancellationToken cancellationToken = default) =>
        Entities.AddAsync(entity, cancellationToken).AsTask();

    public void Update(T entity) => Entities.Update(entity);
    public void Remove(T entity) => Entities.Remove(entity);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        Context.SaveChangesAsync(cancellationToken);
}
