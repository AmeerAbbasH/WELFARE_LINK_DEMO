using Microsoft.EntityFrameworkCore;
using WelfareLinkPRJ.Models;

namespace WelfareLinkPRJ.Data
{
    public class WelfareDbContext :DbContext
    {
        public WelfareDbContext(DbContextOptions<WelfareDbContext> options) : base(options) { }

        public WelfareDbContext() { }

        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<Disbursement> Disbursements { get; set; }
    }
}
