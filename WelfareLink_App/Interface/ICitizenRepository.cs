using System.Collections.Generic;
using System.Threading.Tasks;
using WelfareLink_App.Models;

namespace WelfareLink_App.Repositories
{
    public interface ICitizenRepository
    {
        Task<Citizen> GetByIdAsync(int id);
        Task<Citizen> GetByUserIdAsync(int userId); // Changed to int
        Task AddAsync(Citizen citizen);
        Task UpdateAsync(Citizen citizen);
    }

    
}
