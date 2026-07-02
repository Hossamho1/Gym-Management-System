using GymRoute.BusinessLogic.ViewModel.Member;
using GymRoute.DataAccess.Entities;
using GymRoute.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GymRout.Presentation.Controllers;

public class MemberController(IMemberRepository members) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
       var items = await members.GetAllAsync(cancellationToken);
        return View(items);

    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberViewModel model, CancellationToken cancellationToken)
    {
        return View();
    }
}
