using GymRoute.BusinessLogic.Services;
using GymRoute.BusinessLogic.ViewModel.HealthRecord;
using GymRoute.BusinessLogic.ViewModel.Member;
using Microsoft.AspNetCore.Mvc;

namespace GymRout.Presentation.Controllers;

public class MemberController(IMembrService memberService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var result = await memberService.GetAllAsync(cancellationToken);

        if (result.IsFailure)
        {
            return View(Array.Empty<MemberIndexViewModel>());
        }

        return View(result.Value);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateMemberViewModel
        {
            HealthRecordViewModel = new HealthRecordViewModel()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateMemberViewModel model, CancellationToken cancellationToken)
    {
        model.HealthRecordViewModel ??= new HealthRecordViewModel();

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await memberService.CreateAsync(model, cancellationToken);

        if (result.IsFailure)
        {
            if (!string.IsNullOrEmpty(result.Field))
            {
                ModelState.AddModelError(result.Field, result.Error);
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Error);
            }

            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }
}
