using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Helpers;
using RunGroopWebApp.Services;
using RunInGroup.Data;
using RunInGroup.Data.Interface;
using RunInGroup.Repository;


namespace RunInGroup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<RunDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

                
            });

            builder.Services.AddControllersWithViews();

            // Add Repositories
            builder.Services.AddScoped<IRaceRepository, RaceRepository>();
            builder.Services.AddScoped<IClubRepository, ClubRepository>();

            // Add services to the container.
            builder.Services.AddScoped<IPhotoService , PhotoService>();
           

            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));






            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            Seeding.SeedData(app);


            app.Run();
        }
    }
}