using DomainModel.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PlantTrackerAPI.DomainModel
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Action> Actions { get; set; }
    }
}
