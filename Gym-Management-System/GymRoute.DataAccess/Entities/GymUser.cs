

using GymRoute.DataAccess.EnumGender;
using System.ComponentModel.DataAnnotations;

namespace GymRoute.DataAccess.Entities;

public  abstract class GymUser :BaseEntity
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string phone { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }

    public Address Address;



}
