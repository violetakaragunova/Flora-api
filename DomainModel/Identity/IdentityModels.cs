using Microsoft.AspNetCore.Identity;

namespace DomainModel.Identity
{
    public class UserRole : IdentityUserRole<int>
    {
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
