using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    class AppUserRole : IdentityUserRole<int>
    {
        public AppUser User { get; set; }
        public Role Role { get; set; }
    }
}
