using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WelfareLink_App.Models;
using WelfareLink_App.Services;

namespace WelfareLink_App.Pages.Citizen
{
    public class DashboardModel : PageModel
    {
        private readonly ICitizenService _citizenService;
        private readonly IDocumentService _documentService;

        public DashboardModel(ICitizenService citizenService, IDocumentService documentService)
        {
            _citizenService = citizenService;
            _documentService = documentService;
        }

        public Models.Citizen CitizenProfile { get; set; }
        public IEnumerable<CitizenDocument> Documents { get; set; }
        public int PendingDocuments { get; set; }
        public int ApprovedDocuments { get; set; }
        public int RejectedDocuments { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // For demo purposes, using a hardcoded UserId = 1
            // In production, get this from authenticated user claims
            int currentUserId = 1;

            CitizenProfile = await _citizenService.GetCitizenByUserIdAsync(currentUserId);
            
            if (CitizenProfile == null)
            {
                return RedirectToPage("/Citizen/CreateProfile");
            }

            Documents = await _documentService.GetDocumentsByCitizenIdAsync(CitizenProfile.Id);
            
            PendingDocuments = Documents.Count(d => d.VerificationStatus == "Pending");
            ApprovedDocuments = Documents.Count(d => d.VerificationStatus == "Approved");
            RejectedDocuments = Documents.Count(d => d.VerificationStatus == "Rejected");

            return Page();
        }
    }
}
