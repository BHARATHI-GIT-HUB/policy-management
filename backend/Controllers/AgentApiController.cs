using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RepositryAssignement.BO;
using RepositryAssignement.Repository;
using RepositryAssignement.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepositryAssignement.Controllers
{
    [Route("api/agent")]
    [ApiController]
    public class AgentApiController : ControllerBase
    {
        private readonly PolicyBO policyBO;
        private readonly IPolicyRepository policyRepository;

        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            MaxDepth = 64
        };


        public AgentApiController(IPolicyRepository policyRepository, IHttpContextAccessor httpContext)
        {
            this.policyBO = new PolicyBO(policyRepository, httpContext);
            this.policyRepository = policyRepository;
        }
        // GET: api/<AgentApiController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var agents = policyRepository.FetchAllAgent();

                return Ok(agents);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
     

       

        // PUT api/<AgentApiController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Agent agent)
        {

            var res = policyRepository.UpdateAgent(id, agent);
            if (res >= 0)
            {
                return Ok(new { Message = $"Agent with id {id} updated Successfully" });
            }
            return BadRequest(new { Message = $"Agent with {id} Not Found" });
        }

        // DELETE api/<AgentApiController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int res = policyRepository.DeleteAgent(id);
                if (res == 0)
                {
                    return Ok(new { Message = $"Agent with id {id} Deleted Successfully" });
                }
                else
                {
                    return BadRequest(new { Message = $"Agent with id {id} Not Deleted " });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            };
        }
    }
}
