using GymRoute.BusinessLogic.Services;
using GymRoute.BusinessLogic.ViewModel.HealthRecord;
using GymRoute.BusinessLogic.ViewModel.Member;
using GymRoute.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            HealthRecordViewModel = new CreateHealthRecordViewModel()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateMemberViewModel model, CancellationToken cancellationToken)
    {
        model.HealthRecordViewModel ??= new CreateHealthRecordViewModel();

        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Cannot Create a Member";
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

            TempData["Error"] = "Cannot Create a Member";
            return View(model);     
        }

        TempData["Success"] = "Member created successfully.";

        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var memberDetails = await memberService.GetDetailsAsync(id, cancellationToken);
        if (memberDetails == null)
        {
            return NotFound();
        }

        return View(memberDetails);
    }
    [HttpGet]
    public async Task<IActionResult> HealthRecordDetails(int id, CancellationToken cancellationToken)
    {
        var healthRecord = await memberService.GetHealthRecordByIdAsync(id, cancellationToken);
        if (healthRecord == null)
        {
            return NotFound();
        }

        return View("HealthRecord", healthRecord);
    }

    [HttpGet]
    public async Task<IActionResult> HealthRecord(int id, CancellationToken cancellationToken)
    {
        var healthRecord = await memberService.GetHealthRecordByIdAsync(id, cancellationToken);
        if (healthRecord == null)
            return NotFound();

        return View("HealthRecord", healthRecord);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var member = await memberService.GetForEditAsync(id, cancellationToken);
        if (member == null)
        {
            return NotFound();
        }
        return View(member);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute]int id,EditMemberViewModel editMemberViewModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(editMemberViewModel);
        }
        var result = await memberService.UpdateAsync(id,editMemberViewModel, cancellationToken);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(result.ErrorKey ?? string.Empty, result.Error!);
            TempData["Error"] = "Cannot Update a Member";
            return View(editMemberViewModel);

        }
        TempData["Success"] = "Member updated successfully.";
        return RedirectToAction(nameof(Index));

    }



    [HttpGet]

    public async Task<IActionResult> Delete(int id,CancellationToken ct) 
    {
        var member = await memberService.GetDetailsAsync(id, ct);
        if(member == null)
         return NotFound();
        
        ViewBag.id=member.Id;

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        var result = await memberService.DeleteAsync(id, ct);
        if (!result.IsSuccess)
        {
            TempData["Error"] = "Cannot Delete a Member";
            ViewBag.id = id;    
            return View();
        }
        TempData["Success"] = "Member deleted successfully.";
        return RedirectToAction(nameof(Index));
    }




}
