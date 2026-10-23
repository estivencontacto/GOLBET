using GolBet.Entities.Enums;
using GolBet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GolBet.Web.Controllers;

public class MatchesController(IMatchService matchService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(MatchStatus? status, string? search, CancellationToken cancellationToken)
    {
        ViewBag.CurrentStatus = status;
        ViewBag.CurrentSearch = search?.Trim();
        var matches = await matchService.GetMatchesAsync(status, search, cancellationToken);
        return View(matches);
    }

    [HttpGet]
    public async Task<IActionResult> Detail(int id, CancellationToken cancellationToken)
    {
        var match = await matchService.GetByIdAsync(id, cancellationToken);
        return match is null ? NotFound() : View(match);
    }
}
