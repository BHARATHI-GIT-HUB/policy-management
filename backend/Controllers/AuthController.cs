using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RepositryAssignement.BO;
using RepositryAssignement.Models;
using RepositryAssignement.Repository;
using System.CodeDom;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepositryAssignement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase


    {

        private readonly LoginBO _loginBO = null;
        private readonly IPolicyRepository policyRepository;

     

        public AuthController(ILoginRepository loginRepository, IHttpContextAccessor httpContext)
        {
            _loginBO = new LoginBO(loginRepository);
        }
        [HttpPost("login")]
        public IActionResult Login( Login user)
        {
            try { 
          
            if (user is null)
            {
                return BadRequest("Invalid client request");
            }
            User userData = _loginBO.Login(user.UserName, user.Password);

            if (userData != null)
            {
                    string tokenString  = CreateToken(userData);
                return Ok(new  { Token = tokenString });
            }
            return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }




        private string CreateToken(User user)
        {
            List<System.Security.Claims.Claim> claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(ClaimTypes.Name, user.Username),
                new System.Security.Claims.Claim(ClaimTypes.Role, user.Role.Type),
                new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signinCredentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        // GET: api/<AuthController>
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public IEnumerable<string> Get()
        {
            var authHeader = HttpContext.Request.Headers["Authorization"];
            Console.WriteLine(authHeader);
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        // GET api/<AuthController>/5
        public string Get(int id)
        {
            var authHeader = HttpContext.Request.Headers["Authorization"];
            Console.WriteLine(authHeader);
            return "value";
        }

        // POST api/<AuthController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
