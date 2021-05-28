using Microsoft.AspNetCore.Identity;
using PlantTrackerAPI.DomainModel;
using System;
using System.Collections.Generic;

namespace DomainModel.Identity
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
