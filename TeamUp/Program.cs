using Microsoft.EntityFrameworkCore;
using TeamUpData;
using TeamUpServices.PROJECT.ABSTRACTION;
using TeamUpServices.PROJECT.IMPLEMENTATION;
using TeamUpServices.USERS.ABSTRACTION;
using TeamUpServices.USERS.IMPLEMENTATION;
namespace TeamUp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();
            //// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region  AddDbContext DI
            builder.Services.AddDbContext<TeamUpDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            #endregion

            #region Dependency Injection User  
            builder.Services.AddScoped<IUsersQueries, UsersQueries>();
            builder.Services.AddScoped<IUserCommand, UserCommand>();
            #endregion

            #region Dependency Injection Project  
            builder.Services.AddScoped<IProjectCommand, ProjectCommand>();
            builder.Services.AddScoped<IProjectQueries, ProjectQueries>();
            #endregion



            #region app
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

            #endregion

        }

    }
}
