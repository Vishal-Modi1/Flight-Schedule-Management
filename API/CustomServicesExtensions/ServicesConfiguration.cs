using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Interface;
using Service;
using Service.Interface;

namespace API.CustomServicesExtensions
{
    public static class ServicesConfiguration
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISendMailService, SendMailService>();
            services.AddScoped<IMyAccountService, MyAccountService>();
            services.AddScoped<IInstructorTypeService, InstructorTypeService>();
            services.AddScoped<IAircraftMakeService, AircraftMakeService>();
            services.AddScoped<IAircraftModelService, AircraftModelService>();
            services.AddScoped<IAircraftCategoryService, AircraftCategoryService>();
            services.AddScoped<IAircraftClassService, AircraftClassService>();
            services.AddScoped<IAircraftEquipementTimeService, AircraftEquipementTimeService>();
            services.AddScoped<IAircraftService, AircraftService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IClassificationService, ClassificationService>();
            services.AddScoped<IAirCraftEquipmentService, AirCraftEquipmentService>();
        }

        public static void AddCustomRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IInstructorTypeRepository, InstructorTypeRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IEmailTokenRepository, EmailTokenRepository>();
            services.AddScoped<IMyAccountRepository, MyAccountRepository>();
            services.AddScoped<IAircraftMakeRepository, AircraftMakeRepository>();
            services.AddScoped<IAircraftModelRepository, AircraftModelRepository>();
            services.AddScoped<IAircraftCategoryRepository, AircraftCategoryRepository>();
            services.AddScoped<IAircraftClassRepository, AircraftClassRepository>();
            services.AddScoped<IAircraftEquipmentTimeRepository, AircraftEquipmentTimeRepository>();
            services.AddScoped<IAircraftRepository, AircraftRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IClassificationRepository, ClassificationRepository>();
            services.AddScoped<IAirCraftEquipmentRepository, AirCraftEquipmentRepository>();
        }
    }
}
