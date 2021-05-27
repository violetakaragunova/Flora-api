using DomainModel.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public int Id { get; set; }
        public AppUser User { get; set; }
        public Role Role { get; set; }
    }
}
