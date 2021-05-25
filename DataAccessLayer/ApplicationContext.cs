using DomainModel.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims");
            modelBuilder.Entity<UserToken>().ToTable("UserTokens");

            modelBuilder.Entity<ApplicationUser>().Property(x => x.FullName).HasMaxLength(200).IsRequired();
        }
    }
}
