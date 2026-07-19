using GymRoute.BusinessLogic.Common;
using GymRoute.BusinessLogic.ViewModel.Member;
using GymRoute.BusinessLogic.ViewModel.HealthRecord;
using Microsoft.Identity.Client;

namespace GymRoute.BusinessLogic.Services;

public interface IMembrService
{
    Task<Result<IReadOnlyList<MemberIndexViewModel>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result> CreateAsync(CreateMemberViewModel model, CancellationToken cancellationToken = default);

    Task<MemberDetailsViewModel> GetDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<HealthRecordDetailsViewModel?> GetHealthRecordByIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task<EditMemberViewModel?> GetForEditAsync(int id, CancellationToken cancellationToken = default);

    Task<Result> UpdateAsync(int id,EditMemberViewModel model, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id,   CancellationToken cancellationToken = default);



} 
