using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WelfareLink_App.Data;
using WelfareLink_App.Models;

namespace WelfareLink_App.Repositories
{
    public class CitizenRepository : ICitizenRepository
    {
        private readonly WelfareDbContext _context;

        public CitizenRepository(WelfareDbContext context)
        {
            _context = context;
        }

        public async Task<Citizen> GetByIdAsync(int id)
        {
            return await _context.Citizens
                .Include(c => c.CitizenDocuments)
                .FirstOrDefaultAsync(c => c.Id == id); // Matches your "Id" property
        }

        public async Task<Citizen> GetByUserIdAsync(int userId)
        {
            return await _context.Citizens
                .Include(c => c.CitizenDocuments)
                .FirstOrDefaultAsync(c => c.UserId == userId); // Matches your "UserId" property
        }

        public async Task AddAsync(Citizen citizen)
        {
            await _context.Citizens.AddAsync(citizen);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Citizen citizen)
        {
            _context.Citizens.Update(citizen);
            await _context.SaveChangesAsync();
        }
    }
}
