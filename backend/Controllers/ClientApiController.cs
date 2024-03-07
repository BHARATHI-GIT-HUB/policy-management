using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RepositryAssignement.BO;
using RepositryAssignement.Repository;
using RepositryAssignement.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepositryAssignement.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientApiController: ControllerBase
    {
        private readonly PolicyBO policyBO;
        private readonly IPolicyRepository policyRepository;

        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            MaxDepth = 64
        };


        public ClientApiController(IPolicyRepository policyRepository, IHttpContextAccessor httpContext)
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
                var clients = policyRepository.FetchAllClient();

                return Ok(clients);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }



        // POST api/<ProviderApiController>
        [HttpPut("{id}")]
        public IActionResult Put(int id,  Client client)
        {
            Console.WriteLine(client.MobileNo, " clinet");
            try { 
            var res = policyRepository.UpdateClient(id, client);
            if (res >= 0)
            {
                return Ok(new {Message = $"Client with id {id} updated Succesfully" });
            }
            return BadRequest(new { Message = $"Client with id {id} not  found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }





        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int res = policyRepository.DeleteClient(id);
                if (res == 0)
                {

                    return Ok(new { Message = $"Client with id {id} Deleted Successfully" });
                }
                else
                {
                    return BadRequest(new { Message = $"Client with id {id} Not Deleted " });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            };
        }
    }
}

