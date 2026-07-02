using GymRoute.DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymRoute.DataAccess.Entities;

public class HealthRecord :BaseEntity
{
    public decimal Height { get; set; }
    public decimal Weight { get; set; }

    public BloodType BloodType { get; set; }
    public string? Notes { get; set; }  
    public int MemberId { get; set; }
    public  Member Member { get; set; }=null!;

 
}
