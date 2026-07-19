using System;
using System.Collections.Generic;
using System.Text;

namespace GymRoute.DataAccess.Entities;

public class Member : GymUser
{
    public string? photo { get; set; }
    public DateTime JoinDate { get; set; }
    public HealthRecord? HealthRecord { get; set; }

    public ICollection<MemberShip> Memberships { get; set; } = new List<MemberShip>();

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
