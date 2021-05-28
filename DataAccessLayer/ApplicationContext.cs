using DomainModel.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlantTrackerAPI.DomainModel;

namespace DataAccessLayer
{
    public class ApplicationContext : IdentityDbContext<User, Role, int,
    IdentityUserClaim<int>, DomainModel.Identity.UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Action> Actions { get; set; }
        public DbSet<Need> Needs { get; set; }
        public DbSet<PlantNeed> PlantNeeds { get; set; }
        public DbSet<PlantImage> PlantImages { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");

            modelBuilder.Entity<UserRole>()
                .HasKey(k => new { k.UserId, k.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(u => u.UserId);

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

            modelBuilder.Entity<PlantImage>()
                .HasOne(ur => ur.Plant)
                .WithMany(u => u.Photos)
                .HasForeignKey(ur => ur.Id);


        }
    }
}
