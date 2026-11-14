using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using TeamUpAPI;
using TeamUpAPI.Controllers.Authorization;
using System.Security.Claims;

namespace TeamUp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();

            //#region RateLimiter
            ////  إعدادات حماية الـ API من هجمات الـ Brute-Force
            //builder.Services.AddRateLimiter(options =>
            //{
            //    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            //    options.AddPolicy("AuthLimiter", httpContext =>
            //    {
            //        var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            //        return RateLimitPartition.GetFixedWindowLimiter(
            //            partitionKey: ip,
            //            factory: _ => new FixedWindowRateLimiterOptions
            //            {
            //                PermitLimit = 3,
            //                Window = TimeSpan.FromMinutes(1), // خلال دقيقة واحدة
            //                QueueLimit = 0 // رفض الطلبات الزائدة فوراً دون وضعها في طابور انتظار
            //            });
            //    });
            //});
            //#endregion

            #region RateLimiter
            builder.Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                // 🚨 إضافة "غرفة العمليات": ماذا نفعل عندما نكتشف هجوماً؟
                options.OnRejected = async (context, token) =>
                {
                    // 1. جلب الـ IP الخاص بالمهاجم
                    var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                    // 2. جلب المسار الذي يتعرض للهجوم
                    var path = context.HttpContext.Request.Path;

                    // 3. كتابة تحذير شديد اللهجة في الـ Logs (نواة الـ SIEM)
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                    logger.LogCritical("🚨 DOS ATTACK DETECTED! IP: {IP} is attacking Path: {Path}. Requests are being blocked.", ip, path);

                    // 4. (خطوة مستقبلية) هنا يمكنك إضافة كود لإرسال إيميل أو إشعار Telegram للأدمن!
                    // await EmailService.SendEmergencyAlertAsync(ip, path);

                    // إرجاع الرسالة للمهاجم
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.HttpContext.Response.WriteAsync("Stop. You have been flagged for abusive behavior.", token);
                };

                options.AddPolicy("AuthLimiter", httpContext =>
                {
                    var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: ip,
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 3,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0
                        });
                });
            });
            #endregion

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

            builder.Services.AddTeamUpServices(builder.Configuration);

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

            #region StudentOwnerOrAdminPolicy Requirements

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("StudentOwnerOrAdminPolicy", policy =>
                    policy.Requirements.Add(new StudentOwnerOrAdminRequirement()));
            });

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

            app.UseHttpsRedirection();//هاي لازم تكون موجودة  وتكون قبل ال

            app.UseCors("TeamUpApiCorsPolicy");// هاي انا كتبتها هو موقعها الصح ولازم تعرف انه هاي عشان تطبق ال Cors

            #region RateLimiter Write
            app.UseRateLimiter();

            //  رسالة مخصصة (اختيارية) لتوضيح سبب الحظر دون كشف الأرقام الداخلية للهاكر
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
                {
                    await context.Response.WriteAsync("Too many login attempts. Please try again later.");
                }
            });
            #endregion


            app.UseAuthentication();
            app.UseAuthorization();
            #region (Global 403 Logger)
            app.Use(async (context, next) =>
            {
                await next();

                // إذا حاول شخص الدخول لمسار لا يملك صلاحيته (مثلاً طالب يحاول الدخول لمسار أدمن)
                if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";
                    var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                    var path = context.Request.Path.ToString();

                     app.Logger.LogWarning(
                        "Forbidden access attempted. UserId={UserId}, Path={Path}, IP={IP}",
                        userId, path, ip);
                }
            });
            #endregion
            app.MapControllers();//تكون قبل هاي 


            app.Run();

            #endregion

        }

    }
}
