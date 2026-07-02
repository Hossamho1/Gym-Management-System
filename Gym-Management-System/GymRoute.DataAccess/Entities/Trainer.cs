using GymRoute.DataAccess.Enuml;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymRoute.DataAccess.Entities;

public class Trainer : GymUser
{
    public Speciality Speciality { get; set; }
    public  DateTime HireDate { get; set; }

    public ICollection<Session> Sessions { get; set; } = [];
}
