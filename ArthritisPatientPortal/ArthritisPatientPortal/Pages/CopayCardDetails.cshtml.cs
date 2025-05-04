using ArthritisPatientPortal.Interface;
using ArthritisPatientPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArthritisPatientPortal.Pages
{
    public class CopayCardDetailsModel : PageModel
    {
        private readonly IPatientService _patientService;
        public CopayCardDetailsModel(IPatientService patientService)
        {
            _patientService = patientService;
        }
        public CopayCard CopayCard { get; set; }

      

      
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                CopayCard = await _patientService.GetCopayCardAsync(id);
                return Page();
            }
            catch (KeyNotFoundException)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
