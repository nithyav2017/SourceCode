using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class LoginAuthController : ControllerBase
    {
        private static AuthDBContext _context;
        
        public LoginAuthController(AuthDBContext context)
        {
            _context = context;
        }

       
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person request)
        {

            if (request == null || (request.BusinessEntityID == 0))
            {
                return BadRequest("Invalid credentials");
            }
            var user = await _context.FetchEntities(_context, request.BusinessEntityID);

            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }
            return Ok(user);
        }
    }
}
