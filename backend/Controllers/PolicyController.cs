using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositryAssignement.Models;
using RepositryAssignement.BO;
using RepositryAssignement.Repository;
using System.Diagnostics;
using ClosedXML.Excel;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositryAssignement.Filter;
using RepositryAssignement.Custome_Exception;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Collections;
using DocumentFormat.OpenXml.Spreadsheet;
using RepositryAssignement.Helper;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System.Numerics;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
using System.Globalization;


namespace RepositryAssignement.Controllers
{
    [AuthorizationFilter]
    [Route("[controller]/[action]")]
    public class PolicyController : Controller
    {
        private readonly PolicyBO _policyBO ;
        private readonly IPolicyRepository  _policyRepository ;
        private readonly ILogger<PolicyController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
#pragma warning disable S1450 // Private fields only used as local variables in methods should become local variables
        private readonly ISession session;
#pragma warning restore S1450 // Private fields only used as local variables in methods should become local variables

        readonly string  userId;
        readonly Calculate cal = new Calculate();

        public PolicyController(IPolicyRepository policyRepository, ILogger<PolicyController> logger, IHttpContextAccessor httpContext)
        {
            this._policyBO = new PolicyBO(policyRepository,httpContext);
            this._policyRepository = policyRepository;
            _httpContextAccessor = httpContext;
            _logger = logger;
            this.session = _httpContextAccessor.HttpContext?.Session! ;
            this.userId = session?.GetString("UserId")! ; 
        }

        [HttpGet]
        [RoleBasedAuthAttribute("Client","Admin")]
        public ActionResult Index()
        {
            try
            {
                _logger.LogInformation("Redirected to Policy Enrollment page");

                IEnumerable<Provider> providers = _policyBO.GetAllProvider();

                Dictionary<int, string> providerMap = new Dictionary<int, string>();

                foreach (var provider in providers)
                {

                    providerMap.Add(provider.Id, provider.CompanyName ?? "Default company name");
                }

                ViewBag.ProviderMap = providerMap;

                IEnumerable<Plan>? plans = _policyBO.GetAllPlans();

                if (plans != null)
                {
                plans = plans.OrderBy(pl => pl.ProviderId).ToList();

                    _logger.LogInformation($"Fetched all plans {plans}");
                    return View(plans);
                }
            }
            catch (PolicyManagementException ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.ToString());
            }

            return View();

        }

        [HttpPost]
        public ActionResult Index(IFormCollection collection)
        {
            try
            {
                _logger.LogInformation($"selected plan id {collection["selectedCardId"]}");
              
                int planId = int.Parse(collection["selectedCardId"]!);


                return RedirectToAction("Create", "Policy", new { planId = planId });
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.ToString());
            }
            return View();


        }

        [HttpGet]
        [RoleBasedAuth("Client", "Admin")]

        public ActionResult Create(int planId)
        {
            try
            {

                _logger.LogInformation($"Enrolling to plan with  id {planId}");
                Client client;
                Plan plan;

                if (this.userId == "9")
                {
                     plan = _policyBO.GetPlanByID(planId, 10);
                
                _logger.LogInformation($"plan with id is {plan}");
                Console.WriteLine(userId + " userid ----------"+ planId);

               

                Console.WriteLine(userId + " userid ----------");
                //admin
                
                     client = _policyBO.GetClientById(10);

                }
                else
                {
                     plan = _policyBO.GetPlanByID(planId, int.Parse(this.userId));

                    client = _policyBO.GetClientById(int.Parse(this.userId));
                }


                DateTime cliendDob = DateTime.ParseExact(client.Dob.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                DateTime currentDate = DateTime.Today;

                int age = currentDate.Year - cliendDob.Year;

                int timePeriod = cal.TotalTimePreriod(age);


                @ViewBag.PlanName = plan.PlanName;
                @ViewBag.PlanId = plan.Id;

                // For Range (Range max)
                @ViewBag.CoverageAmount = plan.MaxCoverageAmount / 100000;
                @ViewBag.TimePeriod = timePeriod;

            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.ToString());
            }


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                
                PolicyEnrollment policy = new PolicyEnrollment();
                string? premiumString = collection["Premium"];
                long premium = 0;
                if(premiumString != null)
                {
                    int decimalIndex = premiumString.IndexOf('.') - 1;
                    string premiumBeforeDecimal = decimalIndex >= 0 ? premiumString.Substring(0, decimalIndex + 1) : premiumString;
                    premium = long.Parse(premiumBeforeDecimal);
                }               

                policy.Premium = premium;
                policy.PlanId = int.Parse(collection["PlanId"]!);
                policy.CoverageAmount = long.Parse(collection["coverageAmount"]!);
                policy.CoverageAmount = long.Parse(collection["coverageAmount"]!);
                _logger.LogInformation($"Trying to adding the policy {policy} by to database");


                return RedirectToAction("Submit", "Policy", policy);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.ToString());
            }
            return View();

        }

        [Route("{id}")]
        [HttpGet]
        [RoleBasedAuth("Admin")]

        public ActionResult Edit(int id)
        {
            PolicyEnrollment policy = new PolicyEnrollment();
            try
            {
                _logger.LogInformation($"Trying to Edit plan with  id {id}");
                policy = _policyBO.GetPolicyByID(id);
               
                if(policy != null)
                {

                return View(policy);
                }

            } catch (PolicyNotFoundException ex) {
                TempData["ErrorMessage"] = ex.Message;
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.ToString());
            }
            return View(policy);
        }

        [Route("{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection collection, int id)
        {
            PolicyEnrollment policy = _policyBO.GetPolicyByID(id);
            try
            {
               
                policy.CoverageAmount = long.Parse(collection["CoverageAmount"]!);
                
                policy.CommisionAmount = long.Parse(collection["CommisionAmount"]!);
                policy.Frequency = collection["Frequency"];
                policy.TimePeriod = int.Parse(collection["TimePeriod"]!);

                _policyBO.UpdatePolicy(id, policy);

                _logger.LogInformation($"Succesfully Edited  Policy with  id {id} to {policy}");

                return RedirectToAction("detail", new { id = id });

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                _logger.LogError(ex.Message);
                Console.WriteLine(ex);
            }

            return View(policy);

        }

        

        [Route("{id}")]
        [RoleBasedAuth("Client","Admin")]
        public ActionResult Detail(int id)
        {
            PolicyEnrollment policy = new PolicyEnrollment();
            try
            {
                _logger.LogInformation($"Trying to Get the Detail About plan with id {id}");
                
                policy = _policyBO.GetPolicyByID(id);

                _logger.LogInformation($"Succesfully Fetched the plan with  id {id} : {policy}");

                return View(policy);
            }
            catch (PolicyNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.ToString());
            }
           
            return View(policy);
        }

        [RoleBasedAuth("Admin")]
        public ActionResult Details(IEnumerable<PolicyEnrollment> filterData)
        {
            try
            {
                IEnumerable<City> cities = _policyBO.GetAllCity();
               
                if (cities != null)
                {
                    var selectListItems = cities.Select(city => new SelectListItem
                    {
                        Text = city.Name,
                        Value = city.Id.ToString(),
                    }).ToList();

                    ViewBag.Options = selectListItems;

                    ViewData["SelectedValue"] = "Avadi";
                }

                return View(filterData);

            }
            catch (PolicyManagementException ex)
            {
                _logger.LogError($"{ex.Message}");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.ToString());
            }

            return View();

        }


        [HttpGet]
        [RoleBasedAuth("Admin")]
        public ActionResult Details()
        {
            try
            {
                _logger.LogInformation($"Trying to Get all policy Details ");

                IEnumerable<City> cities = _policyBO.GetAllCity();

                var selectListItems = cities.Select(city => new SelectListItem
                {
                    Text = city.Name,
                    Value = city.Id.ToString(),
                }).ToList();

                ViewBag.Options = selectListItems;
                ViewData["SelectedValue"] = "Avadi";


                IEnumerable<PolicyEnrollment> policies = _policyBO.GetAllPolicies();


                _logger.LogInformation($"Succesfully Fetched All the Details  {policies}");

                return View(policies);

            }
            catch (PolicyNotFoundException ex)
            {
                _logger.LogError($"PolicyNotFound {ex}");
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.ToString());
            }
            return View();

        }

        [HttpGet]
        [RoleBasedAuth("Client", "Admin")]

        public ActionResult Submit(PolicyEnrollment policy)
        {
            try
            {
                _logger.LogInformation($"Submitting View Action method triggered");
                Client client;
                Plan plan;

                if (this.userId == "9")
                {
                    plan = _policyBO.GetPlanByID(policy.PlanId, 10);

                    _logger.LogInformation($"plan with id is {plan}");



                    Console.WriteLine(userId + " userid ----------");
                    //admin

                    client = _policyBO.GetClientById(10);

                }
                else
                {
                    plan = _policyBO.GetPlanByID(policy.PlanId, int.Parse(this.userId));

                    client = _policyBO.GetClientById(int.Parse(this.userId));
                }

                

                DateTime cliendDob = DateTime.ParseExact(client.Dob.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime currentDate = DateTime.Today;

                int age = currentDate.Year - cliendDob.Year;

                int timePeriod = cal.TotalTimePreriod(age);

                ViewBag.TimePeriod = timePeriod;    
                ViewBag.PlanName = plan?.PlanName;
                ViewBag.PlanDescription = plan?.Description;
                ViewBag.PlanId = plan?.Id;
                ViewBag.Coverage = policy.CoverageAmount;

                return View(policy);
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.ToString());
            }
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Submit(IFormCollection collection)
        {
            try
            {
                int frequencyValue = int.Parse(collection["Frequency"]!);
                string actualFrequency = cal.ActualFrequency(frequencyValue);

                PolicyEnrollment policy = new PolicyEnrollment
                {

                    PlanId = int.Parse(collection["PlanId"]!),
/*                    ClientId = int.Parse(HttpContext.Session.GetString("UserId")),
*/                    CoverageAmount = long.Parse(collection["CoverageAmount"]!),
                    TimePeriod = int.Parse(collection["TimePeriod"]!),
                    Premium = long.Parse(collection["Premium"]!),
                    AgentId = 4,
                    Frequency = actualFrequency,
                    CommisionAmount = 1500,
                };

                int id = await _policyBO.InsertPolicy(policy);
                _logger.LogInformation($"Added policy {policy}  to database , policy id {id}");

                TempData["SuccessMessage"] = $"Successfully enrolled in plan {policy.PlanId}";
                return RedirectToAction("detail", new { id = id });

            }
            catch (InvalidOperationException ex)
            {
                
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex);
            }

            return View();
        }


        [Route("{id}")]
        [HttpGet]
        [RoleBasedAuth("Client", "Admin")]

        public ActionResult Delete(int id)
        {
            PolicyEnrollment policy = new PolicyEnrollment();
           
            try {
                _logger.LogInformation($"Trying to Delete the policy by id {id} ");
                
                policy = _policyBO.GetPolicyByID(id);

                return View(policy);
            }
            catch (PolicyManagementException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                _logger.LogError(ex.Message);
                Console.WriteLine(ex);
            }
            catch (PolicyNotFoundException ex) {
                TempData["ErrorMessage"] = ex.Message;
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.ToString());
            }

            return View(policy);
        }

     
        [Route("{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PolicyEnrollment policy)
        {
            
            try
            {
                _policyBO.DeletePolicy(policy);
               
                _logger.LogInformation($"Succesfully Deleted  Policy with  id {policy.Id} ");

                return RedirectToAction("Details");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return View();
        }


        [HttpPost]
        [RoleBasedAuth( "Admin")]

        public FileResult? Export()
        {

            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[12]
            { new DataColumn("EnrollmentId"),
              new DataColumn("PlanName"),
              new DataColumn("ProviderName"),
              new DataColumn("AgentName"),
              new DataColumn("ClientName"),
              new DataColumn("CoverageAmount"),
              new DataColumn("Frequency"),
              new DataColumn("Premium"),
              new DataColumn("CommunitionAmount"),
              new DataColumn("EnrolledOn"),
              new DataColumn("CancelledOn"),
              new DataColumn("ExpiredOn")
            });

            IEnumerable<PolicyEnrollment>? policies = _policyBO.GetAllPolicies();
            if (policies != null)
            {
                foreach (var policy in policies)
                {
                    
                        dt.Rows.Add(policy.Id, policy.Plan?.PlanName, policy.Plan?.Provider?.User?.Username
                            , policy.Agent?.User?.Username, policy.Client?.User?.Username,
                            policy.CommisionAmount, policy.Frequency, policy.Premium,
                            policy.CommisionAmount, policy.EnrolledOn, policy.CancelledOn, policy.ExpiredOn);
                    
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        _logger.LogInformation($"Report Downloaded");

                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
                    }

                }
                

            }
            return null;
        }

            [HttpPost]
            public IActionResult Filter(IFormCollection collection)
            {
                try {
                    string city = collection["SelectedValue"]!;

                    IEnumerable<PolicyEnrollment> filteredPolicy = _policyBO.FilterPolicy(int.Parse(city!));

                    _logger.LogInformation($"Succesfully Filtered Policy with city with id {city}");

                    return Details(filteredPolicy);

                }
                catch (PolicyManagementException ex)
                {
                    _logger.LogError(ex.Message);
                    Console.WriteLine(ex);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    Console.WriteLine(ex);
                }

                return Details();

            }

        [RoleBasedAuth( "Admin")]

        [HttpPost]
            public IActionResult DescOrder(IFormCollection collection)
            {
                try {

                    IEnumerable<PolicyEnrollment> policies = _policyBO.OrderBy();
                    _logger.LogInformation($"Succesfully Ordered  Policy with Coverage Amount ");

                    return Details(policies);

                }
                catch (PolicyManagementException ex)
                {
                    _logger.LogError(ex.Message);
                    Console.WriteLine(ex);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    Console.WriteLine(ex);
                }

                return Details();

            }


        [HttpGet]
        [RoleBasedAuth("Agent", "Admin")]

        public ActionResult Upload( )
        {
            _logger.LogInformation("Opened Bulk Upload page......");
            return View();

        }

    
        [HttpPost]
        public Task<ActionResult> TableData(IList<IFormFile> files)
        {
            _logger.LogInformation("Inserting Bulk Enrollment ......");

            List<Dictionary<string, string>> allTableData = new List<Dictionary<string, string>>();
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

                  }
                }
            }
            catch (PolicyManagementException ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex);
            }
            return BulkUpload(allTableData);
        }




        public async Task<ActionResult> BulkUpload(IList<Dictionary<string, string>> Datas)
        {
                List<int> ids = new List<int>();
            try {
                foreach (var data in Datas)
                {
                    PolicyEnrollment policy = new PolicyEnrollment();
                    policy.PlanId = _policyBO.GetPlanIdByPlanName(data["Plan Name"], data["Provider Name"]);
                    policy.AgentId = _policyBO.GetAgentIdByUserName(data["Agent Name"]);
                    policy.ClientId = _policyBO.GetClientIdByUserName(data["Client Name"]);
                    policy.CoverageAmount = int.Parse(data["Coverage Amount"]);
                    policy.TimePeriod = int.Parse(data["Time Period"]);
                    policy.Frequency = data["Frequency"];

                     int id = await _policyBO.InsertPolicy(policy);
                    ids.Add(id);
                    _logger.LogInformation($"Added policy {policy}  to database , policy id {id}");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.Message);
            }

            string stringId = "";
            foreach (var id in ids)
            {
                stringId += id + " , "; 
            }
            TempData["Message"] = stringId;

            return RedirectToAction("Details");

        }


    }
}
