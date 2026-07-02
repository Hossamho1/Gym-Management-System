using GymRoute.DataAccess.Data.Contexts;
using GymRoute.DataAccess.Entities;
using GymRoute.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymRoute.Presentation.Controllers;

public class PlansController(IGenericRepository<Plan> _PlanRepo) : Controller
{
   
    public async Task<IActionResult> Index()
    {
        var plans = await _PlanRepo.GetAllAsync();
        return View(plans);
    }
    public async Task<IActionResult> Details(int id)
    {

        if (id<=0) {
            return NotFound();
        }

        var plan = await _PlanRepo.GetByIdAsync(id);

        if(plan is null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(plan);




        
    }
}
