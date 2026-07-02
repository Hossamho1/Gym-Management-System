using GymRoute.BusinessLogic.ViewModel.Member;
using GymRoute.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymRoute.BusinessLogic.Services;

public  interface IMembrService
{
    public Task<IReadOnlyList<MemberIndexViewModel>> GetAllAsync(CancellationToken cancellationToken);
    public Task<bool> CreateAsync(CreateMemberViewModel model,CancellationToken cancellationToken=default);

}
