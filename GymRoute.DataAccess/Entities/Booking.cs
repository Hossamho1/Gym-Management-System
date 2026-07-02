using System;
using System.Collections.Generic;
using System.Text;

namespace GymRoute.DataAccess.Entities;

public class Booking : BaseEntity
{
    public DateTime BookingDate { get; set; }
    public bool IsActive { get; set; }

    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public int SessionId { get; set; }
    public Session Session { get; set; } = null!;


}
