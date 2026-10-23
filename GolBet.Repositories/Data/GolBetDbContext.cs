using GolBet.Entities;
using GolBet.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace GolBet.Repositories.Data;

public class GolBetDbContext(DbContextOptions<GolBetDbContext> options) : DbContext(options)
{
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<Bet> Bets => Set<Bet>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasIndex(team => team.Name).IsUnique();
            entity.Property(team => team.Name).HasMaxLength(100).IsRequired();
            entity.Property(team => team.LogoUrl).HasMaxLength(500);
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasIndex(match => match.MatchDateUtc);
            entity.Property(match => match.Venue).HasMaxLength(150).IsRequired();
            entity.Property(match => match.HomeOdds).HasPrecision(8, 2);
            entity.Property(match => match.DrawOdds).HasPrecision(8, 2);
            entity.Property(match => match.AwayOdds).HasPrecision(8, 2);

            entity.HasOne(match => match.HomeTeam)
                .WithMany(team => team.HomeMatches)
                .HasForeignKey(match => match.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(match => match.AwayTeam)
                .WithMany(team => team.AwayMatches)
                .HasForeignKey(match => match.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Bet>(entity =>
        {
            entity.Property(bet => bet.BettorName).HasMaxLength(100).IsRequired();
            entity.Property(bet => bet.Amount).HasPrecision(12, 2);
            entity.Property(bet => bet.Odds).HasPrecision(8, 2);
            entity.HasOne(bet => bet.Match)
                .WithMany(match => match.Bets)
                .HasForeignKey(bet => bet.MatchId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        StampAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void StampAuditFields()
    {
        var utcNow = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = utcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = utcNow;
            }
        }
    }
}
