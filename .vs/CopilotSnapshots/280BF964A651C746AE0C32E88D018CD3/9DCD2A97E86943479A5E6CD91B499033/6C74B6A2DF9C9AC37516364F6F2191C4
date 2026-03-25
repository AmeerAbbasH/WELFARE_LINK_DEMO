using Microsoft.EntityFrameworkCore;
using WELFARE_Application_MVCF.Models;
namespace WELFARE_Application_MVCF.Data
{
    public class WelfareDbContext : DbContext
    {
        public WelfareDbContext(DbContextOptions<WelfareDbContext> options) : base(options)
        {

        }
        public WelfareDbContext() { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<WelfareProgram> Programs { get; set; }
        public DbSet<Resource> Resources { get; set; }
    }
}
