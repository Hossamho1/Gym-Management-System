using GymRoute.BusinessLogic.Common;
using GymRoute.BusinessLogic.ViewModel.Member;
using GymRoute.BusinessLogic.ViewModel.HealthRecord;
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
            Phone = m.Phone,
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

        if (await membersRepo.ExistAsync(m => m.Phone == model.phone, cancellationToken))
        {
            return Result.Failure("A member with this phone number already exists.", nameof(model.phone));
        }

        if (!Enum.TryParse(model.Gender, true, out Gender gender))
        {
            return Result.Failure("Invalid gender selected.", nameof(model.Gender));
        }



        var member = new Member
        {
            Name = model.Name,
            Email = email,
            Phone = model.phone,
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
                BloodType = (BloodType)model.HealthRecordViewModel.BloodType,
                Height = model.HealthRecordViewModel.Height,
                Weight = model.HealthRecordViewModel.Weight,
                Notes = model.HealthRecordViewModel.Notes,
            }
        };

        await membersRepo.AddAsync(member, cancellationToken);
        await membersRepo.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<MemberDetailsViewModel?> GetDetailsAsync(
      int id,
      CancellationToken cancellationToken = default)
    {
        var member = await membersRepo.GetByIdAsync(
            id: id,

            cancellationToken: cancellationToken,
            includes: m => m.Memberships
        );

        if (member is null)
            return null;

        var today = DateOnly.FromDateTime(DateTime.Today);

        var activeMembership = member.Memberships
            .FirstOrDefault(m => DateOnly.FromDateTime(m.EndDate) >= today);

        return new MemberDetailsViewModel
        {
            Id = id,
            Name = member.Name,
            PhotoUrl = member.photo,
            Email = member.Email,
            Phone = member.Phone,
            Gender = member.Gender.ToString(),
            DateOfBirth = member.DateOfBirth.ToShortDateString(),
            Address = $"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}",

            PlanName = activeMembership?.Plan.Name ?? "No Active Plan",
        };
    }

    public async Task<HealthRecordDetailsViewModel?> GetHealthRecordByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var member = await membersRepo.GetByIdAsync(
            id: id,
            includes: m => m.HealthRecord,
            cancellationToken: cancellationToken);

        if (member is null || member.HealthRecord is null)
            return null;

        var hr = member.HealthRecord;
        return new HealthRecordDetailsViewModel
        {
            Height = hr.Height,
            Weight = hr.Weight,
            BloodType = hr.BloodType.ToString(),
            Notes = hr.Notes,
        };
    }

    public async Task<EditMemberViewModel> GetForEditAsync(int id, CancellationToken cancellationToken = default)
    {
        var member = await membersRepo.GetByIdAsync(id, cancellationToken);

        if (member is null) return null;

        return new EditMemberViewModel
        {
            Name = member.Name,
            PhotoUrl = member.photo,
            Email = member.Email,
            phone = member.Phone,
            BuildingNumber = member.Address?.BuildingNumber ?? 0,
            City = member.Address?.City ?? string.Empty,
            Street = member.Address?.Street ?? string.Empty
        };
    }



    public async Task<Result> UpdateAsync(int id ,
     EditMemberViewModel model,
     CancellationToken cancellationToken = default)
    {
        var member = await membersRepo.GetByIdAsync(id, cancellationToken);

        if (member is null)
            return Result.Failure("Member not found.", nameof(id));

        if (member.Name != model.Name)
            Result.Failure("Name cannot be changed ", nameof(model.Name));



        var normalizedEmail = model.Email.Trim().ToLowerInvariant();
        var normalizedPhone = model.phone.Trim();

        if (await membersRepo.IsEmailTakenAsync(normalizedEmail, model.Id))
            return Result.Failure("This email is already registered.", nameof(model.Email));

        if (await membersRepo.IsPhoneTakenAsync(normalizedPhone, model.Id))
            return Result.Failure("This phone number is already registered.", nameof(model.phone));

        member.Email = normalizedEmail;
        member.Phone = normalizedPhone;
        

        member.Address = new Address
        {
            BuildingNumber = model.BuildingNumber,
            City = model.City.Trim(),
            Street = model.Street.Trim()
        };

        await membersRepo.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(
     int id,
     CancellationToken cancellationToken = default)
    {
        Member? member = await membersRepo.GetByIdAsync(
            id,
            includes: m => m.Bookings,
            cancellationToken: cancellationToken);

        if (member is null)
            return Result.Failure("Member not found.", nameof(id));

        if (await membersRepo.HasUpBookingAsync(id,cancellationToken))
            return Result.Failure(
                "Cannot delete member with upcoming bookings.",
                nameof(id));

        await membersRepo.SoftDeleteAsync(member, cancellationToken);

        await membersRepo.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
