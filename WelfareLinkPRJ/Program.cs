using Microsoft.EntityFrameworkCore;
using WelfareLinkPRJ.Data;
using WelfareLinkPRJ.Interfaces;
using WelfareLinkPRJ.Repositories;
using WelfareLinkPRJ.Services;

namespace WelfareLinkPRJ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            

            //Db reg
            builder.Services.AddDbContext<WelfareDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

   

            //Dependency Injection
            builder.Services.AddScoped<IBenefitRepository, BenefitRepository>();
            builder.Services.AddScoped<IBenefitService, BenefitService>();

            builder.Services.AddScoped<IDisbursementRepository, DisbursementRepository>();
            builder.Services.AddScoped<IDisbursementService, DisbursementService>();

            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
