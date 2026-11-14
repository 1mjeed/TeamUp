using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using TeamUpData;
using TeamUpServices.Auth.ABSTRACTION;
using TeamUpServices.Auth.IMPLEMENTATION;
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

            #region Swagger 
            builder.Services.AddSwaggerGen(options =>
            {
                // ===============================
                // 1) Define the JWT Bearer security scheme
                // ===============================
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token. The 'Bearer ' prefix is added automatically."
                });

                // ===============================
                // 2) Require the Bearer scheme — Swashbuckle v10 delegate syntax
                // ===============================
                options.AddSecurityRequirement(document =>
                {
                    var schemeRef = new OpenApiSecuritySchemeReference("Bearer", document);
                    return new OpenApiSecurityRequirement
                    {
                        [schemeRef] = []
                    };
                });
            });
            #endregion

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

            #region Dependency Injection Catagory  
            builder.Services.AddScoped<ICategoryCommand, CategoryCommand>();
            builder.Services.AddScoped<ICategoryQueries, CategoryQueries>();
            #endregion

            #region Dependency Injection Role  
            builder.Services.AddScoped<IRoleCommand, RoleCommand>();
            builder.Services.AddScoped<IRoleQueries, RoleQueries>();
            #endregion

            #region Dependency Injection Registration  
            builder.Services.AddScoped<IRegistrationCommand, RegistrationCommand>();
            builder.Services.AddScoped<IRegistrationQueries, RegistrationQueries>();
            #endregion

            builder.Services.AddScoped<IAuthService, AuthService>();

            #region JWT Authentication Configuration 
            // قراءة الإعدادات من appsettings.json
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Key"];

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                    };
                });
            #endregion

            #region policy CORS
            //builder.Build(); لازم قبل ال 
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("TeamUpApiCorsPolicy", policy =>
                {
                    policy
                        .WithOrigins(
                            "https://localhost:71572",//origin
                            "http://localhost:5281" // origin
                        )
                        .AllowAnyHeader()//اسمح للـ Frontend بإرسال أي Headers (مثل توكن الـ JWT).
                        .AllowAnyMethod();//اسمح بجميع العمليات (GET, POST, PUT, DELETE).
                });
            });

            #endregion

            builder.Services.AddHttpContextAccessor();

            #region app
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();//هاي لازم تكون موجودة  وتكون قبل ال

            app.UseCors("TeamUpApiCorsPolicy");// هاي انا كتبتها هو موقعها الصح ولازم تعرف انه هاي عشان تطبق ال Cors

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();//تكون قبل هاي 

            app.Run();

            #endregion

        }

    }
}
