using center.Api.Entities;
using center.Api.Models;
using center.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace center.Api.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IStudentRepo _studentRepo;

        public AuthenticationController(IConfiguration configuration , IStudentRepo studentRepo)
        {
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
            _studentRepo = studentRepo ?? throw new ArgumentNullException();
        }
        public class AuthenticationRequestBody
        {
            public int UserName { get; set; }
            public string? Password { get; set; }
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string> > Authenticate(
          AuthenticationRequestBody authenticationRequestBody)
        {
            // Step 1: validate the username/password
            var user = ValidateUserCredentials(
                authenticationRequestBody.UserName,
                authenticationRequestBody.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            // Step 2: create a token

            var secretKey = _configuration["Authentication:SecretForKey"];
            if (string.IsNullOrEmpty(secretKey))
            { 
                return BadRequest("Invalid secret key configuration.");
            }

 
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("id", user.Id.ToString())); 




            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
               .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        private async Task<ActionResult<Student>> ValidateUserCredentials(int userName, string? password)
        {
            
            if(!await _studentRepo.StudentExistsAsync(userName))
            {
                return null;
            }
            return await _studentRepo.GetStudentByIdAsync(userName);

        }
    }


}
