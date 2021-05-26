using DomainModel.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ApplicationContext : IdentityDbContext<AppUser, Role, int,
    IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Action> Actions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims");
            modelBuilder.Entity<UserToken>().ToTable("UserTokens");

            modelBuilder.Entity<User>().Property(x => x.FullName).HasMaxLength(200).IsRequired();
        }
    }
}
