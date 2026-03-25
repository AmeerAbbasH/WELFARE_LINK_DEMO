using WELFARE_Application_MVCF.Data;
using WELFARE_Application_MVCF.Interfaces;
using WELFARE_Application_MVCF.Models;
using Microsoft.EntityFrameworkCore;
namespace WELFARE_Application_MVCF.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly WelfareDbContext _context;
        public UserRepository(WelfareDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
