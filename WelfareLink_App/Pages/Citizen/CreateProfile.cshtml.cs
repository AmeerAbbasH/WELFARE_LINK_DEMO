using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WelfareLink_App.Models;
using WelfareLink_App.Services;
using System.ComponentModel.DataAnnotations;

namespace WelfareLink_App.Pages.Citizen
{
    public class CreateProfileModel : PageModel
    {
        private readonly ICitizenService _citizenService;

        public CreateProfileModel(ICitizenService citizenService)
        {
            _citizenService = citizenService;
        }

        [BindProperty]
        public CreateCitizenViewModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // For demo purposes, using a hardcoded UserId = 1
            int currentUserId = 1;

            var citizen = new Models.Citizen
            {
                UserId = currentUserId,
                Name = Input.Name,
                DateOfBirth = Input.DateOfBirth,
                Address = Input.Address,
                ContactInfo = Input.ContactInfo,
                Status = "Active",
                CreatedAt = DateTime.UtcNow
            };

            var success = await _citizenService.CreateCitizenProfileAsync(citizen);

            if (success)
            {
                TempData["SuccessMessage"] = "Profile created successfully!";
                return RedirectToPage("/Citizen/Dashboard");
            }

            ModelState.AddModelError(string.Empty, "Failed to create profile.");
            return Page();
        }

        public class CreateCitizenViewModel
        {
            [Required]
            [StringLength(50)]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Date of Birth")]
            public DateTime DateOfBirth { get; set; }

            [Required]
            [StringLength(300)]
            public string Address { get; set; }

            [Required]
            [StringLength(50)]
            [Display(Name = "Contact Information (Phone/Email)")]
            public string ContactInfo { get; set; }
        }
    }
}
