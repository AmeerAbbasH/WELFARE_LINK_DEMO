using WelfareLink_App.Models;

namespace WelfareLink_App.Services
{
    public interface IDocumentService
    {
        Task<IEnumerable<CitizenDocument>> GetDocumentsByCitizenIdAsync(int citizenId);
        Task<CitizenDocument> GetDocumentByIdAsync(int documentId);
        Task<bool> UploadDocumentAsync(CitizenDocument document, IFormFile file);
        Task<bool> DeleteDocumentAsync(int documentId);
        Task<string> SaveFileAsync(IFormFile file, string docType);
    }
}
