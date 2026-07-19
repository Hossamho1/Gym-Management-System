using GymRoute.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GymRoute.DataAccess.Repositories;

public interface IMemberRepository : IGenericRepository<Member>
{
    Task<bool> IsEmailTakenAsync(string email,int? excludeId=null, CancellationToken cancellationToken = default);
    Task<bool> IsPhoneTakenAsync(string phone, int? excludeId=null, CancellationToken cancellationToken = default);
    Task<Member?> GetWithMembershipsAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> HasUpBookingAsync(int memberId, CancellationToken cancellationToken = default);

}
