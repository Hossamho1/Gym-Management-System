using GymRoute.DataAccess.Data.Contexts;
using GymRoute.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymRoute.DataAccess.Repositories;

public class MemberRepository(GymDbContext dbContext) : Repository<Member>(dbContext), IMemberRepository
{



}
