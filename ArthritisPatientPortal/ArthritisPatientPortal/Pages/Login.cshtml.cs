using ArthritisPatientPortal.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ArthritisPatientPortal.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly IPatientService _patientService;

        public LoginModel(IPatientService patientService )
        {
            //_signInManager = signInManager;
            _patientService = patientService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string PIN { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                //var result = await _signInManager.PasswordSignInAsync(Input.Email,
                //    Input.Password, Input.RememberMe, lockoutOnFailure: false);

                var result = await _patientService.GetPatientAsync(Input.PIN, Input.Email);

                if (result != null)
                {
                    return LocalRedirect(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return Page();
        }
    }
}
