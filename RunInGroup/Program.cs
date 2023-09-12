using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Helpers;
using RunGroopWebApp.Services;
using RunInGroup.Data;
using RunInGroup.Data.Interface;
using RunInGroup.Models;
using RunInGroup.Repository;


namespace RunInGroup
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<RunDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });
            //IdentityFrameWork
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<RunDbContext>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();


            builder.Services.AddControllersWithViews();

            // Add Repositories
            builder.Services.AddScoped<IRaceRepository, RaceRepository>();
            builder.Services.AddScoped<IClubRepository, ClubRepository>();
            builder.Services.AddScoped<IDashboardRepository , DashboradRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // Add services to the container.
            builder.Services.AddScoped<IPhotoService, PhotoService>();


            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));







            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "account",
                pattern: "Account/{action=Login}/{id?}",
                defaults: new { controller = "Account" });


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            // Seeding.SeedData(app);
            await Seeding.SeedUsersAndRolesAsync(app);



            app.Run();
        }
    }
}