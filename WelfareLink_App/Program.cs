using WelfareLink_App.Data;
using WelfareLink_App.Repositories;
using WelfareLink_App.Services;
using Microsoft.EntityFrameworkCore;
namespace WelfareLink_App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //DB CONTEXT
            builder.Services.AddDbContext<WelfareDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register Repositories
            builder.Services.AddScoped<ICitizenRepository, CitizenRepository>();
            builder.Services.AddScoped<ICitizenDocumentRepository, CitizenDocumentRepository>();

            // Register Services
            builder.Services.AddScoped<ICitizenService, CitizenService>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

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
            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
