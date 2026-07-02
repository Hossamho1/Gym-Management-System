using GymRoute.BusinessLogic.Common;
using GymRoute.BusinessLogic.ViewModel.Member;

namespace GymRoute.BusinessLogic.Services;

public interface IMembrService
{
    Task<Result<IReadOnlyList<MemberIndexViewModel>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result> CreateAsync(CreateMemberViewModel model, CancellationToken cancellationToken = default);
}
