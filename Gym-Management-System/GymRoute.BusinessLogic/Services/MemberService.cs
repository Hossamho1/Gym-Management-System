using GymRoute.BusinessLogic.ViewModel.Member;
using GymRoute.DataAccess.Entities;
using GymRoute.DataAccess.Enum;
using GymRoute.DataAccess.EnumGender;
using GymRoute.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GymRoute.BusinessLogic.Services;

internal class MembrService(MemberRepository membersRepo) : IMembrService
{

    public async Task<IReadOnlyList<MemberIndexViewModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        var members = await membersRepo.GetAllAsync(cancellationToken);
        return members.Select(m => new MemberIndexViewModel
        {
            Id = m.Id,
            Name = m.Name,
            Phone = m.phone,
            Email = m.Email,
            JoinDate = m.JoinDate,

            PhotoUrl = m.photo,
           
            Gender = m.Gender.ToString(),
        }).ToList();
    }
    public async Task<bool> CreateAsync(CreateMemberViewModel model, CancellationToken cancellationToken = default)
    {

        var email= model.Email.Trim().ToLower();
        if (await membersRepo.ExistAsync(m => m.Email == email, cancellationToken))
        {
            return false;

        }
        if (await membersRepo.ExistAsync(m => m.phone == model.phone, cancellationToken))
        {
            return false;
        }

        if(!Enum.TryParse(model.Gender,true, out Gender gender ))
        {
            return false;
        }
        if (!Enum.TryParse(model.HealthRecordViewModel.BloodType, true, out BloodType bloodType))
        {
            return false;
        }

        var member = new Member 
        {
            Name = model.Name,
            Email = email,
            phone = model.phone,
            DateOfBirth = model.DateOfBirth,
            Gender = gender,
            JoinDate = DateTime.UtcNow,
            Address = new Address
            {
                BuildingNumber = model.BuildingNumber,
                City = model.City,
                Street = model.Street
            }
            ,
            HealthRecord = new HealthRecord
            {
                BloodType = bloodType,
                Height = model.HealthRecordViewModel.Height,
                Weight = model.HealthRecordViewModel.Weight,
                Notes = model.HealthRecordViewModel.Notes,
            }

        };


        await membersRepo.AddAsync(member, cancellationToken);
        await membersRepo.SaveChangesAsync(cancellationToken);
        return true;
    }

}
