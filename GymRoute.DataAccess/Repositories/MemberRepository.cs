using GymRoute.DataAccess.Data.Contexts;
using GymRoute.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymRoute.DataAccess.Repositories;

public class MemberRepository : Repository<Member>, IMemberRepository
{
    private readonly GymDbContext _dbContext;

    public MemberRepository(GymDbContext gymDbContext) : base(gymDbContext)
    {
        _dbContext = gymDbContext;
    }



    public Task<Member?> GetWithMembershipsAsync(int id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Set<Member>()
            .Include(m => m.Memberships)
            .ThenInclude(ms => ms.Plan)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }


    public async Task<bool> HasUpBookingAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        return await _dbContext.Set<Booking>()
            .AnyAsync(
                b => b.MemberId == id && b.Session.EndDate >= now,
                cancellationToken);
    }

    public Task<bool> IsEmailTakenAsync(
        string normalizedEmail,
        int? excludeId = null,
        CancellationToken cancellationToken = default)
        => _dbContext.Set<Member>().AnyAsync(
            m => m.Email == normalizedEmail &&
                 (!excludeId.HasValue || m.Id != excludeId.Value),
            cancellationToken);
    public Task<bool> IsPhoneTakenAsync(
       string phone,
       int? excludeId = null,
       CancellationToken cancellationToken = default)
       => _dbContext.Set<Member>().AnyAsync(
           m => m.Phone == phone &&
                (!excludeId.HasValue || m.Id != excludeId.Value),
           cancellationToken);
}
