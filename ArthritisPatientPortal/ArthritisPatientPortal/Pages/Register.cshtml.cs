using ArthritisPatientPortal.Interface;
using ArthritisPatientPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArthritisPatientPortal.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IPatientService _patientService;

        public RegisterModel(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [BindProperty]
        public Patient Patient { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _patientService.RegisterPatientAsync(Patient);
            return RedirectToPage("Dashboard", new  { pin = Patient.Pin});
        }
    }
}
