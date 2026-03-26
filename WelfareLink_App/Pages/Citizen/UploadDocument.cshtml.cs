using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WelfareLink_App.Models;
using WelfareLink_App.Services;
using System.ComponentModel.DataAnnotations;

namespace WelfareLink_App.Pages.Citizen
{
    public class UploadDocumentModel : PageModel
    {
        private readonly ICitizenService _citizenService;
        private readonly IDocumentService _documentService;

        public UploadDocumentModel(ICitizenService citizenService, IDocumentService documentService)
        {
            _citizenService = citizenService;
            _documentService = documentService;
        }

        [BindProperty]
        public DocumentUploadViewModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // For demo purposes, using a hardcoded UserId = 1
            int currentUserId = 1;

            var citizen = await _citizenService.GetCitizenByUserIdAsync(currentUserId);
            
            if (citizen == null)
            {
                return RedirectToPage("/Citizen/CreateProfile");
            }

            Input = new DocumentUploadViewModel { CitizenId = citizen.Id };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Input.FileUpload == null || Input.FileUpload.Length == 0)
            {
                ModelState.AddModelError("Input.FileUpload", "Please select a file to upload.");
                return Page();
            }

            var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png", ".doc", ".docx" };
            var fileExtension = Path.GetExtension(Input.FileUpload.FileName).ToLowerInvariant();
            
            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("Input.FileUpload", "Invalid file type. Allowed types: PDF, JPG, JPEG, PNG, DOC, DOCX");
                return Page();
            }

            if (Input.FileUpload.Length > 10 * 1024 * 1024) // 10 MB
            {
                ModelState.AddModelError("Input.FileUpload", "File size cannot exceed 10 MB.");
                return Page();
            }

            var document = new CitizenDocument
            {
                CitizenId = Input.CitizenId,
                DocType = Input.DocType
            };

            var success = await _documentService.UploadDocumentAsync(document, Input.FileUpload);

            if (success)
            {
                TempData["SuccessMessage"] = "Document uploaded successfully!";
                return RedirectToPage("/Citizen/DocumentStatus");
            }

            ModelState.AddModelError(string.Empty, "Failed to upload document.");
            return Page();
        }

        public class DocumentUploadViewModel
        {
            public int CitizenId { get; set; }

            [Required]
            [Display(Name = "Document Type")]
            public string DocType { get; set; }

            [Required]
            [Display(Name = "Select File")]
            public IFormFile FileUpload { get; set; }
        }
    }
}
