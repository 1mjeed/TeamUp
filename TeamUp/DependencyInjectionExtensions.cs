using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TeamUpAPI.Controllers.Authorization;
using TeamUpData;
using TeamUpServices.Authorization.ABSTRACTION;
using TeamUpServices.Authorization.IMPLEMENTATION;
using TeamUpServices.Category.ABSTRACTION;
using TeamUpServices.Category.IMPLEMENTATION;
using TeamUpServices.PROJECT.ABSTRACTION;
using TeamUpServices.PROJECT.IMPLEMENTATION;
using TeamUpServices.REGISTRATIONS.ABSTRACTION;
using TeamUpServices.REGISTRATIONS.IMPLEMENTATION;
using TeamUpServices.ROLES.ABSTRACTION;
using TeamUpServices.ROLES.IMPLEMENTATION;
using TeamUpServices.USERS.ABSTRACTION;
using TeamUpServices.USERS.IMPLEMENTATION;

namespace TeamUpAPI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddTeamUpServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region  AddDbContext DI
            services.AddDbContext<TeamUpDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            #endregion

            #region Dependency Injection User  
            services.AddScoped<IUsersQueries, UsersQueries>();
            services.AddScoped<IUserCommand, UserCommand>();
            #endregion

            #region Dependency Injection Project  
            services.AddScoped<IProjectCommand, ProjectCommand>();
            services.AddScoped<IProjectQueries, ProjectQueries>();
            #endregion

            #region Dependency Injection Catagory  
            services.AddScoped<ICategoryCommand, CategoryCommand>();
            services.AddScoped<ICategoryQueries, CategoryQueries>();
            #endregion

            #region Dependency Injection Role  
            services.AddScoped<IRoleCommand, RoleCommand>();
            services.AddScoped<IRoleQueries, RoleQueries>();
            #endregion

            #region Dependency Injection Registration  
            services.AddScoped<IRegistrationCommand, RegistrationCommand>();
            services.AddScoped<IRegistrationQueries, RegistrationQueries>();
            #endregion

            #region Dependency Injection AuthorizationHandler 

            services.AddSingleton<IAuthorizationHandler, StudentOwnerOrAdminHandler>();
            #endregion

            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
