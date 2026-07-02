using System;
using System.Collections.Generic;
using System.Text;

namespace GymRoute.DataAccess.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } =null!;
    public DateTime? DeletedAt { get; set; } = null!;
    public bool IsDeleted { get; set; }



}
