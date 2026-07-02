using GymRoute.BusinessLogic.Common;
using GymRoute.BusinessLogic.ViewModel.Member;
using GymRoute.DataAccess.Entities;
using GymRoute.DataAccess.Enum;
using GymRoute.DataAccess.EnumGender;
using GymRoute.DataAccess.Repositories;

namespace GymRoute.BusinessLogic.Services;

public class MembrService(IMemberRepository membersRepo) : IMembrService
{
    public async Task<Result<IReadOnlyList<MemberIndexViewModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var members = await membersRepo.GetAllAsync(cancellationToken);
        var viewModels = members.Select(m => new MemberIndexViewModel
        {
            Id = m.Id,
            Name = m.Name,
            Phone = m.phone,
            Email = m.Email,
            JoinDate = m.JoinDate,
            PhotoUrl = m.photo,
            Gender = m.Gender.ToString(),
        }).ToList();

        return Result<IReadOnlyList<MemberIndexViewModel>>.Success(viewModels);
    }

    public async Task<Result> CreateAsync(CreateMemberViewModel model, CancellationToken cancellationToken = default)
    {
        var email = model.Email.Trim().ToLower();

        if (await membersRepo.ExistAsync(m => m.Email == email, cancellationToken))
        {
            return Result.Failure("A member with this email already exists.", nameof(model.Email));
        }

        if (await membersRepo.ExistAsync(m => m.phone == model.phone, cancellationToken))
        {
            return Result.Failure("A member with this phone number already exists.", nameof(model.phone));
        }

        if (!Enum.TryParse(model.Gender, true, out Gender gender))
        {
            return Result.Failure("Invalid gender selected.", nameof(model.Gender));
        }

        if (!Enum.TryParse(model.HealthRecordViewModel.BloodType, true, out BloodType bloodType))
        {
            return Result.Failure("Invalid blood type selected.", $"{nameof(model.HealthRecordViewModel)}.{nameof(model.HealthRecordViewModel.BloodType)}");
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
            },
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

        return Result.Success();
    }
}
