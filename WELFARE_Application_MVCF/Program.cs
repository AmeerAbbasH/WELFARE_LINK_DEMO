using Microsoft.EntityFrameworkCore;
using WELFARE_Application_MVCF.Data;
using WELFARE_Application_MVCF.Interfaces;
using WELFARE_Application_MVCF.Repository;
using WELFARE_Application_MVCF.Services;
namespace WELFARE_Application_MVCF
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

            //Dependency Injection - Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProgramRepository, ProgramRespository>();
            builder.Services.AddScoped<IResourceRepository, ResourceRepository>();

            //Dependency Injection - Services
            builder.Services.AddScoped<IProgramService, ProgramService>();
            builder.Services.AddScoped<IResourceService, ResourceService>();

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
