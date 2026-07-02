using System;
using System.Collections.Generic;
using System.Text;

namespace GymRoute.DataAccess.Entities;

public class MemberShip : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MemberId { get; set; }
    public Member Member { get; set; }

    public int PlanId { get; set; }
    public Plan Plan { get; set; } = null!;

}
