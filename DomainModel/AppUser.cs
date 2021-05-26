using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    class AppUser : IdentityUser
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
        public ICollection<Action> Actions { get; set; }
    }
}
