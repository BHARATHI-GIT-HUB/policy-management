using System.Configuration;
using Microsoft.EntityFrameworkCore;
using RepositryAssignement.Repository;
using RepositryAssignement.Models;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Serilog;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace RepositryAssignement
{
    public class Program
    {
        protected Program() { }

        public static void Main(string[] args)
        {
         

            var builder = WebApplication.CreateBuilder(args);


            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Logging.
            builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));
            
            //DBContext
            builder.Services.AddDbContext<InsuranceDbContext>();

            // DI
            builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            /*builder.Services.AddControllers().AddNewtonsoftJson();
*/

            builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
      
            builder.Services.AddSession(options =>
            {
                options.Cookie.IsEssential = true; // make the session cookie essential
            });

            builder.Services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30));

            // Add HttpContextAccessor
            builder.Services.AddHttpContextAccessor();


            
            //Jwt Config
            builder.Services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://192.168.0.152:7154",
            ValidAudience = "http://192.168.0.152:7154",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345superSecretKey@345"))
        };
    });

            var app = builder.Build();

            app.UseCors(options =>
            {
                /*options.WithOrigins("");*/
                options.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSerilogRequestLogging();

            /*app.UseHttpsRedirection();*/
            app.UseStaticFiles();


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

         /*   app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });*/
            app.Run("http://192.168.0.152:7154");

            app.Run();
        }
    }
}
