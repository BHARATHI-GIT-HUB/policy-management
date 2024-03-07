using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RepositryAssignement.BO;
using RepositryAssignement.Models;
using RepositryAssignement.Repository;
using System.CodeDom;

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
                
            
                var userInfo = new 
                {
                    Name = userData.Username,
                    UserId = userData.Id,
                    Role = userData.Role.Type,
                };

                var userInfoJson = System.Text.Json.JsonSerializer.Serialize(userInfo);

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: "",
                    audience: "",
                    claims: null ,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
               
                );
                
                tokenOptions.Payload["userInfo"] = userInfoJson;

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new  { Token = tokenString });
            }
            return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Authorize]
        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
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
