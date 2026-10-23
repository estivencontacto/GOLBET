using System.Globalization;
using GolBet.Repositories.Data;
using GolBet.Repositories.Implementations;
using GolBet.Repositories.Interfaces;
using GolBet.Services.Implementations;
using GolBet.Services.Interfaces;
using GolBet.Services.Mappings;
using Microsoft.EntityFrameworkCore;

var colombianCulture = CultureInfo.GetCultureInfo("es-CO");
CultureInfo.DefaultThreadCurrentCulture = colombianCulture;
CultureInfo.DefaultThreadCurrentUICulture = colombianCulture;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GolBetDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GolBetConnection")));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IMatchRepository, MatchRepository>();
builder.Services.AddAutoMapper(configuration => { }, typeof(MappingProfile).Assembly);
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IAdminMatchService, AdminMatchService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GolBetDbContext>();
    await DatabaseSeeder.SeedAsync(context);
}

app.Run();
