**UseCase**: Handle user registrations with potential injections. Use DataAnnotations in C# models, validate on server, and 
             parameterize SQL queries.

The most secure recommended approach is .NET Core Identity Provider , which handles most of the concerns like Password hashing 
with PBKDF2,  Salted hashed and User Management by itslef. When implementing registration without Identity Provider, it has to 
handle these protection manually.

Because EF Core Identity Provider
  - Parameterized all SQL queries , so there won't be SQL injection risk.
  - Validations are enforced at server side with Data Annotations 
  - Prevent mass and over-posting ( meaning , When an attacker creates a request with form values, .NET Core automatically maps those
  -                                 fields into object properties. This can lead to over‑posting: the attacker could pass a property
  -                                 value for the property IsAdmin = true, even though the client should not be able to set it. As a
  -                                 result, the attacker could gain privileges and manipulate the database).

In addtion to this approach, 
  - Prevent XSS: Use razor generated HTML encoding (HTMLSanitizer).
  - Anti-Forgery Token Vlaidation: Always include @HTML.AntiForgeryToken() in forms and [ValidateAntiForgeryToken]
     on POST actions.
  - Use AspNetcoreRateLimit to prevent brute-force.
  - Configure stringer password rules thrugh IdentityOPtions.

#### ASP.NET Core Identity Services registration and configurations of its options
[Ref: https://github.com/nithyav2017/SourceCode/blob/Dotnet/UseCases/WebApplication/CodeSample/WebApplication/Program.cs]

#### - /API/RegisterUser
`      [HttpPost("RegisterUser")]
      
        public async Task<IActionResult>  UserRegistration(RegistrationDTO model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            
            model.PasswordSalt = GenerateSalt.CreateSalt();

            var user = new IdentityUser { UserName = model.EmailAddress, Email = model.EmailAddress };
            
            var hasher = new PasswordHasher<IdentityUser>();

            user.PasswordHash = hasher.HashPassword(user, model.PasswordHash  );
          
            await _registrationService.CreateAsync(user,model);                     
            
            return Ok("User registered successfully");
        }
'
#### Save User Registration using DbContext :

[Ref:https://github.com/nithyav2017/SourceCode/blob/Dotnet/UseCases/WebApplication/CodeSample/WebApplication/Services/RegistrationService.cs]


