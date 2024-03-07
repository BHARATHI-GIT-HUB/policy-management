using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RepositryAssignement.BO;
using RepositryAssignement.Repository;
using RepositryAssignement.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepositryAssignement.Controllers
{
    [Route("api/provider")]
    [ApiController]
    public class ProviderApiController : ControllerBase
    {
        private readonly PolicyBO policyBO;
        private readonly IPolicyRepository policyRepository;

        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            MaxDepth = 64
        };


        public ProviderApiController(IPolicyRepository policyRepository, IHttpContextAccessor httpContext)
        {
            this.policyBO = new PolicyBO(policyRepository, httpContext);
            this.policyRepository = policyRepository;
        }
        // GET: api/<ProviderApiController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var provider = policyRepository.FetchAllProvider();

                return Ok(provider);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }



        // POST api/<ProviderApiController>
        [HttpPut("{id}")]
        public IActionResult Put(int id, Provider provider)
        {
            var res = policyRepository.UpdateProvider(id, provider);
            if (res >= 0)
            {
                return Ok(new { Message = $"Provider with id {id} updated Successfully" });
            }
            return BadRequest(new { Message = $"Provider with id {id} Not Found " });
        }





        [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            int res = policyRepository.DeleteProvider(id);
            if (res == 0)
            {
                return Ok(new { Message = $"Provider with id {id} Deleted Successfully" });
            }
            else
            {
                return BadRequest(new { Message = $"Provider with id {id} Not Deleted " });

            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        };
    }
    }
}

