
using Microsoft.EntityFrameworkCore;
using GymRoute.DataAccess.Data.Contexts;
using GymRoute.DataAccess.Entities;

namespace GymRoute.DataAccess.Repositories;

public class PlanRepository(GymDbContext dbContext)  : Repository<Plan>(dbContext),IPlanRepository
{ 
    public GymDbContext context = dbContext;
    public async Task Add(Plan plan)
   => await  context.AddAsync(plan);
    public  void Delete(Plan plan)
    {
           context.Remove(plan);
    }

    public async Task<IEnumerable<Plan>> GetAllAsync()=> await context.Plans.ToListAsync();
    
       
    

    public async Task<Plan?> GetByIdAsync(int id)=> await context.Plans. FirstOrDefaultAsync(p => p.Id == id);

  

    public async Task Update(Plan plan)
    {
        context.Plans.Update(plan);
    }


    public Task<int> SaveChangesAsync() => context.SaveChangesAsync();

    public Task<IReadOnlyList<Plan>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Plan> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Plan> GetByIdIncludingDeletedAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
