using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GolBet.Repositories.Data;

public class GolBetDbContextFactory : IDesignTimeDbContextFactory<GolBetDbContext>
{
    public GolBetDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<GolBetDbContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GolBetDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True")
            .Options;

        return new GolBetDbContext(options);
    }
}
