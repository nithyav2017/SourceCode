using PatientPortalAPI.Data;
using PatientPortalAPI.Helpers;
using PatientPortalAPI.Services;
using clsArthritisPatient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PatientPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;
        private readonly Patient _patient;
        private readonly AuthService _authService;
        public AuthController(AppDbContext appDbContext, IConfiguration configuration, Patient patient, AuthService authService)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
            _patient = patient;
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] Patient patient)
        {
            patient.PinHash = BCrypt.Net.BCrypt.HashPassword(patient.PinHash);
            _appDbContext.patients.Add(patient);
            await _appDbContext.SaveChangesAsync();
            return Ok(patient);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Pin))
                return BadRequest("Email and Pin is Required");

            var patient = await _appDbContext.patients.SingleOrDefaultAsync(p => p.Email == login.Email);

            if (patient == null)
            {
                return Unauthorized("Incorrect Credential");
            }

            if (!BCrypt.Net.BCrypt.Verify(login.Pin, patient.PinHash))
                return Unauthorized("Incorrect Credential");

            var token = _authService.LoginAsync(login.Email, login.Pin);

            return Ok(new { token });
        }
    }
}
