using GolBet.Entities.Enums;
using GolBet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GolBet.Web.Controllers;

public class MatchesController(IMatchService matchService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(MatchStatus? status, CancellationToken cancellationToken)
    {
        ViewBag.CurrentStatus = status;
        var matches = await matchService.GetMatchesAsync(status, cancellationToken);
        return View(matches);
    }
}
