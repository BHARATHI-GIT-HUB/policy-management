using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using RepositryAssignement.BO;
using RepositryAssignement.Models;
using RepositryAssignement.Repository;
using System.Diagnostics;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepositryAssignement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class ApiController : ControllerBase
    {
        private readonly PolicyBO policyBO;
        private readonly IPolicyRepository policyRepository ;

        private readonly JsonSerializerOptions options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 64 
            };


        public ApiController(IPolicyRepository policyRepository, IHttpContextAccessor httpContext)
        {
            this.policyBO = new PolicyBO(policyRepository, httpContext);
            this.policyRepository = policyRepository;
        }      
        
        // GET: api/<PolicyEnrollementController>
        [HttpGet]
        [Route("All")]
        public IActionResult Get()
        {
            var policies = policyBO.GetAllPolicies();           
            string json = JsonSerializer.Serialize(policies, options);
            return Ok(json);
        }

        // GET api/<PolicyEnrollementController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var policies = policyBO.GetPolicyByID(id);
                
            string json = JsonSerializer.Serialize(policies,options);
            return Ok(json);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<PolicyEnrollementController>
        [HttpPost]

        public async Task<IActionResult> Post( PolicyEnrollment policy)
        {
          
            try
            {

            if (policy == null)
            {
                return BadRequest("Data not found error");
            }
            else if (policy.CoverageAmount == 0)
            {
                return BadRequest("Coverage Amount Should be greated than 1");
            }
            else
            {

                int res = await policyBO.InsertPolicy(policy);
                Console.WriteLine(res +" respnse------------------------------");
                if (res > 0)
                {
                    return Ok($"Received policy enrollment: Id={res}, PlanId={policy.PlanId}, AgentId={policy.AgentId}, ClientId={policy.ClientId}, CoverageAmount={policy.CoverageAmount}");

                }
                return BadRequest($"Received data not inserted");
            }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                    }
        }

        // PUT api/<PolicyEnrollementController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, PolicyEnrollment policyEnrollment)
        {
            var res = policyBO.UpdatePolicy(id, policyEnrollment);
            if(res >= 0)
            {
                return Ok($"Policy with {id} updated Succesfully");
            }
            return BadRequest($"Policy with {id} Not Found");
        }

        // DELETE api/<PolicyEnrollementController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            PolicyEnrollment policy = policyBO.GetPolicyByID(id);
            var res = policyBO.DeletePolicy(policy);

            if(res >= 0)
            {
                return Ok($"Data with id {id} Deleted Succuessfully");
            }
            else
            {
                return Ok($"Data with id {id} Not Deleted ");

            }
        }


        [HttpGet("[action]/{id}")]
        public IActionResult Filter(int id)
        {
            
            var policies = policyBO.FilterPolicy(id);

            string json = JsonSerializer.Serialize(policies, options);

            return Ok(json);
        }

        [HttpGet("[action]")]
        public IActionResult Order()
        {
            var policies = policyBO.OrderBy();

            string json = JsonSerializer.Serialize(policies, options);

            return Ok(json);
        }


    }
}
