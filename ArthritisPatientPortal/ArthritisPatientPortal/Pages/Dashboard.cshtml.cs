using ArthritisPatientPortal.Interface;
using ArthritisPatientPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArthritisPatientPortal.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly IPatientService _patientService;

        public DashboardModel(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public Patient Patient { get; set; }

        public async Task<IActionResult> OnGetAsync(string pin)
        {
            try
            {
                Patient = await _patientService.GetPatientByPinAsync(pin);
                return Page();
            }
            catch (KeyNotFoundException)
            {
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostAsync(string pin)
        {
            try
            {
                Patient = await _patientService.GetPatientByPinAsync(pin);
                var copayCard = await _patientService.GenerateCopayCardAsync(Patient.Id);
                Patient.CopayCardId = copayCard.id.ToString();
                return RedirectToPage("CopayCardDetails", new {id=copayCard.id});
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
