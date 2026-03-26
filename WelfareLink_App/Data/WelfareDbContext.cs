using Microsoft.EntityFrameworkCore;
using WelfareLink_App.Models;
namespace WelfareLink_App.Data
{
    public class WelfareDbContext : DbContext
    {
        public WelfareDbContext(DbContextOptions<WelfareDbContext> options) : base(options)
        {
        }
        public WelfareDbContext() { }
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<CitizenDocument> CitizenDocuments { get; set; }
        
    }

}
