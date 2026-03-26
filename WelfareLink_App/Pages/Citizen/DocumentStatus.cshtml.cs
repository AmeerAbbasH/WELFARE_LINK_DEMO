using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WelfareLink_App.Models;
using WelfareLink_App.Services;

namespace WelfareLink_App.Pages.Citizen
{
    public class DocumentStatusModel : PageModel
    {
        private readonly ICitizenService _citizenService;
        private readonly IDocumentService _documentService;

        public DocumentStatusModel(ICitizenService citizenService, IDocumentService documentService)
        {
            _citizenService = citizenService;
            _documentService = documentService;
        }

        public IEnumerable<CitizenDocument> Documents { get; set; }
        public string FilterStatus { get; set; }

        public async Task<IActionResult> OnGetAsync(string status = "")
        {
            // For demo purposes, using a hardcoded UserId = 1
            int currentUserId = 1;

            var citizen = await _citizenService.GetCitizenByUserIdAsync(currentUserId);
            
            if (citizen == null)
            {
                return RedirectToPage("/Citizen/CreateProfile");
            }

            var allDocuments = await _documentService.GetDocumentsByCitizenIdAsync(citizen.Id);
            
            FilterStatus = status;
            
            if (!string.IsNullOrEmpty(status))
            {
                Documents = allDocuments.Where(d => d.VerificationStatus.Equals(status, StringComparison.OrdinalIgnoreCase))
                                       .OrderByDescending(d => d.UploadedDate);
            }
            else
            {
                Documents = allDocuments.OrderByDescending(d => d.UploadedDate);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int documentId)
        {
            var success = await _documentService.DeleteDocumentAsync(documentId);
            
            if (success)
            {
                TempData["SuccessMessage"] = "Document deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete document.";
            }

            return RedirectToPage();
        }
    }
}
