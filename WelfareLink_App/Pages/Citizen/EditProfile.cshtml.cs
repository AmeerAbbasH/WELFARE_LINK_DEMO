using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WelfareLink_App.Models;
using WelfareLink_App.Services;
using System.ComponentModel.DataAnnotations;

namespace WelfareLink_App.Pages.Citizen
{
    public class EditProfileModel : PageModel
    {
        private readonly ICitizenService _citizenService;

        public EditProfileModel(ICitizenService citizenService)
        {
            _citizenService = citizenService;
        }

        [BindProperty]
        public CitizenProfileViewModel Input { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // For demo purposes, using a hardcoded UserId = 1
            int currentUserId = 1;

            var citizen = await _citizenService.GetCitizenByUserIdAsync(currentUserId);
            
            if (citizen == null)
            {
                return RedirectToPage("/Citizen/CreateProfile");
            }

            Input = new CitizenProfileViewModel
            {
                Id = citizen.Id,
                Name = citizen.Name,
                DateOfBirth = citizen.DateOfBirth,
                Address = citizen.Address,
                ContactInfo = citizen.ContactInfo,
                Status = citizen.Status
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var citizen = await _citizenService.GetCitizenByIdAsync(Input.Id);
            
            if (citizen == null)
            {
                ModelState.AddModelError(string.Empty, "Citizen profile not found.");
                return Page();
            }

            citizen.Name = Input.Name;
            citizen.DateOfBirth = Input.DateOfBirth;
            citizen.Address = Input.Address;
            citizen.ContactInfo = Input.ContactInfo;
            citizen.Status = Input.Status;

            var success = await _citizenService.UpdateCitizenProfileAsync(citizen);

            if (success)
            {
                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToPage("/Citizen/Dashboard");
            }

            ModelState.AddModelError(string.Empty, "Failed to update profile.");
            return Page();
        }

        public class CitizenProfileViewModel
        {
            public int Id { get; set; }

            [Required]
            [StringLength(50)]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Date of Birth")]
            public DateTime DateOfBirth { get; set; }

            [StringLength(300)]
            public string Address { get; set; }

            [StringLength(50)]
            [Display(Name = "Contact Information")]
            public string ContactInfo { get; set; }

            [StringLength(50)]
            public string Status { get; set; }
        }
    }
}
