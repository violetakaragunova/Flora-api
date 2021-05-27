using DomainModel.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlantTrackerAPI.DomainModel;

namespace DataAccessLayer
{
    public class ApplicationContext : IdentityDbContext<AppUser, Role, int,
    IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Action> Actions { get; set; }
        public DbSet<Need> Needs { get; set; }
        public DbSet<PlantNeed> PlantNeeds { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*modelBuilder.Entity<AppUser>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims");
            modelBuilder.Entity<UserToken>().ToTable("UserTokens");*/

            modelBuilder.Entity<AppUser>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.User)
            .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<Role>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.Role)
            .HasForeignKey(ur => ur.RoleId);


            modelBuilder.Entity<Action>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.Actions)
                .HasForeignKey(ur => ur.Id);



            modelBuilder.Entity<Action>()
                .HasOne(ur => ur.Plant)
                .WithMany(u => u.Actions)
                .HasForeignKey(ur => ur.Id);



            modelBuilder.Entity<Action>()
                .HasOne(ur => ur.Need)
                .WithMany(u => u.Actions)
                .HasForeignKey(ur => ur.Id);


            modelBuilder.Entity<PlantNeed>()
                .HasOne(ur => ur.Need)
                .WithMany(u => u.PlantNeeds)
                .HasForeignKey(ur => ur.Id);


            modelBuilder.Entity<PlantNeed>()
                .HasOne(ur => ur.Plant)
                .WithMany(u => u.PlantNeeds)
                .HasForeignKey(ur => ur.PlantId);


            modelBuilder.Entity<Plant>()
                .HasOne(ur => ur.Room)
                .WithMany(u => u.Plants)
                .HasForeignKey(ur => ur.Id);


            modelBuilder.Entity<Photo>()
                .HasOne(ur => ur.Plant)
                .WithMany(u => u.Photos)
                .HasForeignKey(ur => ur.Id);


        }
    }
}
