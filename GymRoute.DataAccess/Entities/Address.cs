using System;
using System.Collections.Generic;
using System.Text;

namespace GymRoute.DataAccess.Entities
{
    public class Address
    {
        public string City { get; set; } = null!;

        public string Street { get; set; } = null!;

        public int BuildingNumber { get; set; }
    }
}
