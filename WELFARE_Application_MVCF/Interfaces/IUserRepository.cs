using WELFARE_Application_MVCF.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace WELFARE_Application_MVCF.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}
