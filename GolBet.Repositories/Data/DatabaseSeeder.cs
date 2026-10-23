using GolBet.Entities;
using GolBet.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace GolBet.Repositories.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(GolBetDbContext context, CancellationToken cancellationToken = default)
    {
        await context.Database.MigrateAsync(cancellationToken);

        if (!await context.Teams.AnyAsync(cancellationToken))
        {
            context.Teams.AddRange(
                new Team { Name = "Atlético Nacional", LogoUrl = "/images/teams/nacional.svg" },
                new Team { Name = "Millonarios", LogoUrl = "/images/teams/millonarios.svg" },
                new Team { Name = "América de Cali" },
                new Team { Name = "Independiente Medellín" },
                new Team { Name = "Junior" },
                new Team { Name = "Deportivo Cali" },
                new Team { Name = "Once Caldas" },
                new Team { Name = "Deportes Tolima" });

            await context.SaveChangesAsync(cancellationToken);
        }

        if (await context.Matches.AnyAsync(cancellationToken))
        {
            return;
        }

        var teams = await context.Teams.ToDictionaryAsync(team => team.Name, cancellationToken);
        var utcNow = DateTime.UtcNow;

        context.Matches.AddRange(
            NewMatch(teams, "Atlético Nacional", "Millonarios", utcNow.AddDays(2), "Atanasio Girardot", 1, MatchStatus.Scheduled, 1.85m, 3.20m, 4.10m),
            NewMatch(teams, "América de Cali", "Atlético Nacional", utcNow.AddDays(5), "Pascual Guerrero", 2, MatchStatus.Scheduled, 2.25m, 3.05m, 3.10m),
            NewMatch(teams, "Junior", "Deportivo Cali", utcNow.AddMinutes(-35), "Metropolitano", 1, MatchStatus.Live, 1.70m, 3.45m, 4.75m, 1, 0),
            NewMatch(teams, "Millonarios", "Independiente Medellín", utcNow.AddDays(-2), "El Campín", 1, MatchStatus.Finished, 1.95m, 3.15m, 3.80m, 2, 1),
            NewMatch(teams, "Once Caldas", "Deportes Tolima", utcNow.AddDays(-4), "Palogrande", 1, MatchStatus.Finished, 2.40m, 2.95m, 2.95m, 0, 0),
            NewMatch(teams, "Independiente Medellín", "América de Cali", utcNow.AddDays(8), "Atanasio Girardot", 3, MatchStatus.Scheduled, 2.05m, 3.10m, 3.55m));

        await context.SaveChangesAsync(cancellationToken);
    }

    private static Match NewMatch(
        IReadOnlyDictionary<string, Team> teams,
        string home,
        string away,
        DateTime dateUtc,
        string venue,
        int matchday,
        MatchStatus status,
        decimal homeOdds,
        decimal drawOdds,
        decimal awayOdds,
        int? homeScore = null,
        int? awayScore = null) => new()
        {
            HomeTeamId = teams[home].Id,
            AwayTeamId = teams[away].Id,
            MatchDateUtc = dateUtc,
            Venue = venue,
            Matchday = matchday,
            Status = status,
            HomeOdds = homeOdds,
            DrawOdds = drawOdds,
            AwayOdds = awayOdds,
            HomeScore = homeScore,
            AwayScore = awayScore
        };
}
