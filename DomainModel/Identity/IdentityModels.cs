using Microsoft.AspNetCore.Identity;
using PlantTrackerAPI.DomainModel;

namespace DomainModel.Identity
{
    public class UserRole : IdentityUserRole<int>
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
    public class UserClaim : IdentityUserClaim<int>
    {
    }

    public class UserLogin : IdentityUserLogin<int>
    {
    }
    public class RoleClaim : IdentityRoleClaim<int>
    {
    }
    public class UserToken : IdentityUserToken<int>
    {
    }
}
