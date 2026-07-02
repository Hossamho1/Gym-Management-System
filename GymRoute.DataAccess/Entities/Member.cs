using System;
using System.Collections.Generic;
using System.Text;

namespace GymRoute.DataAccess.Entities;

public class Member : GymUser
{
    public string? photo {get; set;}
    public DateTime JoinDate {get; set;}
    public HealthRecord HealthRecord {get; set;}
    // healthrecord  

    //ICollection => membetships
    ICollection<Member> Members { get; set; } = [];
    //ICollection => Bookings

    ICollection<Booking> Bookings { get; set; } = [];

}
