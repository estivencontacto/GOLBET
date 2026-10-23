using GolBet.Entities.Enums;
using GolBet.Services.DTOs;
using GolBet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GolBet.Web.Controllers;

public class AdminMatchesController(IAdminMatchService adminService, IMatchService matchService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken) =>
        View(await matchService.GetMatchesAsync(cancellationToken: cancellationToken));

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        await LoadOptionsAsync(cancellationToken);
        return View("Form", new MatchFormDto { MatchDate = DateTime.Now.AddDays(1) });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MatchFormDto model, CancellationToken cancellationToken)
    {
        ValidateTeams(model);
        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync(cancellationToken);
            return View("Form", model);
        }

        await adminService.CreateAsync(model, cancellationToken);
        TempData["Message"] = "Partido creado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var model = await adminService.GetForEditAsync(id, cancellationToken);
        if (model is null) return NotFound();
        await LoadOptionsAsync(cancellationToken);
        return View("Form", model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(MatchFormDto model, CancellationToken cancellationToken)
    {
        ValidateTeams(model);
        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync(cancellationToken);
            return View("Form", model);
        }

        if (!await adminService.UpdateAsync(model, cancellationToken)) return NotFound();
        TempData["Message"] = "Partido actualizado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        if (!await adminService.DeleteAsync(id, cancellationToken)) return NotFound();
        TempData["Message"] = "Partido eliminado.";
        return RedirectToAction(nameof(Index));
    }

    private void ValidateTeams(MatchFormDto model)
    {
        if (model.HomeTeamId == model.AwayTeamId)
            ModelState.AddModelError(nameof(model.AwayTeamId), "El equipo visitante debe ser diferente al local.");
    }

    private async Task LoadOptionsAsync(CancellationToken cancellationToken)
    {
        var teams = await adminService.GetTeamsAsync(cancellationToken);
        ViewBag.Teams = new SelectList(teams, nameof(TeamOptionDto.Id), nameof(TeamOptionDto.Name));
        ViewBag.Statuses = new SelectList(Enum.GetValues<MatchStatus>());
    }
}
