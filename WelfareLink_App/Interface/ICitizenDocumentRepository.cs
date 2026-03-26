using System.Collections.Generic;
using System.Threading.Tasks;
using WelfareLink_App.Models;

namespace WelfareLink_App.Repositories
{
    

    public interface ICitizenDocumentRepository
    {
        Task<IEnumerable<CitizenDocument>> GetByCitizenIdAsync(int citizenId);
        Task<CitizenDocument> GetByIdAsync(int documentId); // Changed to int
        Task AddAsync(CitizenDocument document);
        Task DeleteAsync(int documentId); // Changed to int
    }
}
