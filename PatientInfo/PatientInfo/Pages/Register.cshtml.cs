using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PatientInfo.Data;
using PatientInfo.Models;
using System.Runtime.InteropServices;

namespace PatientInfo.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
      
        [BindProperty]
        public RegistrationViewModel RegistrationViewModel { get; set; }   
        
        public RegisterModel(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager,ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public ActionResult  OnGet()
        {
            RegistrationViewModel = new RegistrationViewModel();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.ClearValidationState(nameof(RegisterModel));
            if (TryValidateModel(RegistrationViewModel,nameof(RegistrationViewModel)))
            {
                var user = new ApplicationUser
                {   
                    UserName = RegistrationViewModel.Email,
                    Email = RegistrationViewModel.Email,
                    PatientDetails = new PatientDetails
                    {
                        DemographicInfo = RegistrationViewModel.DemographicInfo,
                        HCPSpecialty = RegistrationViewModel.HCPSpecialty,
                        Indication = RegistrationViewModel.Indication,
                        InsuranceType = RegistrationViewModel.InsuranceType,
                        ConsentForEmail = RegistrationViewModel.ConsentForEmail,
                        ConsentForText = RegistrationViewModel.ConsentForText
                        }
                    };

                var hashedPin = new PasswordHasher<ApplicationUser>().HashPassword(user, RegistrationViewModel.PIN.ToString());
                user.PasswordHash = hashedPin;

                var result = await _userManager.CreateAsync(user);

               // var applicationUser = new ApplicationUser
               // {
               //     Id= Guid.NewGuid().ToString(),
               //     UserName = RegistrationViewModel.Email,
               //     Email = RegistrationViewModel.Email,
               //     PasswordHash=RegistrationViewModel.Password,
               //     PINHash=hashedPin                  

               // };
               // _context.User.Add(applicationUser);
               //var result =  await _context.SaveChangesAsync();


                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "Home");
                }

                //foreach(var error in result.Errors)
                //{
                //    ModelState.AddModelError(String.Empty,error.Description);
                //}
            }
            return RedirectToPage("./Index"); ;

        }

    }
}
