using DomainModel.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlantTrackerAPI.DomainModel;

namespace DataAccessLayer
{
    public class ApplicationContext : IdentityDbContext<User, Role, int,
    IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
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
        public DbSet<Month> Months { get; set; }
        public DbSet<FrequencyType> FrequencyTypes { get; set; }

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
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<Action>()
                .HasOne(ur => ur.Plant)
                .WithMany(u => u.Actions)
                .HasForeignKey(ur => ur.PlantId);

            modelBuilder.Entity<Action>()
                .HasOne(ur => ur.Need)
                .WithMany(u => u.Actions)
                .HasForeignKey(ur => ur.NeedId);

            modelBuilder.Entity<PlantNeed>()
                .HasOne(ur => ur.Need)
                .WithMany(u => u.PlantNeeds)
                .HasForeignKey(ur => ur.NeedId);

            modelBuilder.Entity<PlantNeed>()
                .HasOne(ur => ur.Plant)
                .WithMany(u => u.PlantNeeds)
                .HasForeignKey(ur => ur.PlantId);

            modelBuilder.Entity<PlantNeed>()
                .HasOne(ur => ur.FrequencyType)
                .WithMany(u => u.PlantNeeds)
                .HasForeignKey(ur => ur.FrequencyTypeId);

            modelBuilder.Entity<Plant>()
                .HasOne(ur => ur.Room)
                .WithMany(u => u.Plants)
                .HasForeignKey(ur => ur.RoomId);

            modelBuilder.Entity<PlantImage>()
                .HasOne(ur => ur.Plant)
                .WithMany(u => u.Photos)
                .HasForeignKey(ur => ur.PlantId);

            modelBuilder.Entity<Month>()
                .HasData(
                new Month { Id = 1, Name = "January"},
                new Month { Id = 2, Name = "February" },
                new Month { Id = 3, Name = "March" },
                new Month { Id = 4, Name = "April" },
                new Month { Id = 5, Name = "May" },
                new Month { Id = 6, Name = "June" },
                new Month { Id = 7, Name = "July" },
                new Month { Id = 8, Name = "August" },
                new Month { Id = 9, Name = "September" },
                new Month { Id = 10, Name = "October" },
                new Month { Id = 11, Name = "November" },
                new Month { Id = 12, Name = "December" }
                );

            modelBuilder.Entity<FrequencyType>()
                .HasData(
                new FrequencyType { Id = 1, Type="Daily", Days=1},
                new FrequencyType { Id = 2, Type = "Weekly", Days=7 },
                new FrequencyType { Id = 3, Type = "Monthly", Days=30 }
                );
        }
    }
}
