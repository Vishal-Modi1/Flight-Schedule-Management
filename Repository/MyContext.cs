using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using ViewModels.VM;

namespace Repository
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        public MyContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("configuration.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<InstructorType> InstructorTypes { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<UserSearchList> UserSearchList { get; set; }

        public DbSet<UserRolePermissionVM> UserRolePermissionList { get; set; }

        public DbSet<EmailToken> EmailTokens { get; set; }

        public DbSet<InstructorTypeVM> InstructorTypeVM { get; set; }

        public DbSet<AirCraft> AirCrafts { get; set; }

        public DbSet<AircraftMake> AircraftMakes { get; set; }

        public DbSet<AircraftModel> AircraftModels { get; set; }
        
        public DbSet<AircraftEquipmentTime> AircraftEquipmentTimes { get; set; }

        public DbSet<AircraftCategory> AircraftCategories { get; set; }

        public DbSet<AircraftClass> AircraftClasses { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Classification> Classifications { get; set; }

        public DbSet<AirCraftEquipment> AircraftEquipments { get; set; }

        public DbSet<FlightSimulatorClass> FlightSimulatorClasses { get; set; }

        public DbSet<UserRolePermission> UserRolePermissions { get; set; }
        
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<ModuleDetail> ModuleDetails { get; set; }
    }

}
