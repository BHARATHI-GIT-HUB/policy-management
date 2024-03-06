using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using RepositryAssignement.BO;
using RepositryAssignement.Models;
using RepositryAssignement.Repository;
using System.Diagnostics;
using System.Net;
using System.Web.Helpers;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using RepositryAssignement.Custome_Exception;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepositryAssignement.Controllers
{
    [Route("api/policy")]
    [ApiController]
    
    public class ApiController : ControllerBase
    {
        private readonly PolicyBO policyBO;
        private readonly IPolicyRepository policyRepository ;

        private readonly JsonSerializerOptions options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                MaxDepth = 64
        };


        public ApiController(IPolicyRepository policyRepository, IHttpContextAccessor httpContext)
        {
            this.policyBO = new PolicyBO(policyRepository, httpContext);
            this.policyRepository = policyRepository;
        }      
        
        // GET: api/<PolicyEnrollementController>
        [HttpGet]
        public IActionResult Get()
        {
            var policies = policyRepository.FetchAllOptimized();
            string json = JsonSerializer.Serialize(policies, options);
            return Ok(policies);
        }

        [HttpGet]
        [Route("~/api/plan")]
        public IActionResult GetAllPlans()
        {
            var policies = policyRepository.FetchAllPlan();
            return Ok(policies);
        }


        // GET api/<PolicyEnrollementController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var policies = policyBO.GetPolicyByID(id);
                
            string json = JsonSerializer.Serialize(policies,options);
            return Ok(policies);
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
            try { 
            Console.WriteLine(id + "on update " + policyEnrollment.Frequency +" frequency "+ policyEnrollment.CoverageAmount+" "+ policyEnrollment.TimePeriod);
            var res = policyBO.UpdatePolicy(id, policyEnrollment);
            if (res >= 0)
            {
                return Ok(new { Message = $"Policy with {id} updated Succesfully" });
            }

            return BadRequest(new { Message = $"Policy with {id} Not Found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            };
        }

        // DELETE api/<PolicyEnrollementController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try { 
            PolicyEnrollment policy = policyBO.GetPolicyByID(id);
            var res = policyBO.DeletePolicy(policy);
            if(res == 0)
            {
                    return Ok(new { Message = $"Data with id {id} Deleted Successfully" });
                }
                else
            {
                return BadRequest(new { Message = $"Data with id {id} Not Deleted " });

            }
            }
            catch (Exception ex) {
                return BadRequest(new { Message = ex.Message });
                    };
        }


        [HttpGet("[action]/{id}")]
        public IActionResult Filter(int id)
        {
            
            var policies = policyBO.FilterPolicy(id);

            string json = JsonSerializer.Serialize(policies, options);

            return Ok(policies);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromForm] List<IFormFile> files)
        {
            List<Dictionary<string, string>> allTableData = new List<Dictionary<string, string>>();
            List<int> ids = new List<int>();


            if (files == null || !files.Any())
            {
                return BadRequest(new {Message = "File is not selected or empty"});
            }

        
            try
            {

                foreach (IFormFile file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        Dictionary<string, string> tableData = new Dictionary<string, string>();

                        using (PdfReader reader = new PdfReader(file.OpenReadStream()))
                        {
                            PdfDocument pdfDoc = new PdfDocument(reader);

                            Console.WriteLine("pdf lines : " + pdfDoc.GetNumberOfPages());
                            // Extract text from each page
                            for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                            {
                                string text = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i));

                                // Parse text to extract table data
                                string[] lines = text.Split('\n');
                                int col = 1;
                                string value = "";
                                foreach (var line in lines)
                                {
                                    string key = "";

                                    if (col == 1)
                                    {
                                        value = line.TrimEnd();
                                    }
                                    else
                                    {
                                        key = line.Substring(0, line.LastIndexOf(':', line.Length - 1)).TrimEnd();
                                        tableData[key] = value;
                                        col = 0;
                                        value = "";

                                    }
                                    col += 1;

                                }
                            }

                            allTableData.Add(tableData);

                            pdfDoc.Close();
                        }

                        foreach (var data in allTableData)
                        {
                            PolicyEnrollment policy = new PolicyEnrollment();
                            policy.PlanId = policyBO.GetPlanIdByPlanName(data["Plan Name"], data["Provider Name"]);
                            policy.AgentId = policyBO.GetAgentIdByUserName(data["Agent Name"]);
                            policy.ClientId = policyBO.GetClientIdByUserName(data["Client Name"]);
                            policy.CoverageAmount = int.Parse(data["Coverage Amount"]);
                            policy.TimePeriod = int.Parse(data["Time Period"]);
                            policy.Frequency = data["Frequency"];

                            int id = await policyBO.InsertPolicy(policy);
                            ids.Add(id);


                        }

                    }
                }
            }
            catch (PolicyManagementException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

            return Ok(new { Message = $"Enrolled policy succuessfully"  ,enrolledids = ids});
        }
    }
}
