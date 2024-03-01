using RepositryAssignement.Repository;
using RepositryAssignement.Models;
using RepositryAssignement.Custome_Exception;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Security.Permissions;
using System.Numerics;
using RepositryAssignement.Helper;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Globalization;

namespace RepositryAssignement.BO
{
    public class PolicyBO
    {
        public readonly IPolicyRepository _politcyRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession session;

        readonly string userId;
        readonly private Calculate cal = new Calculate();
        public PolicyBO(IPolicyRepository politcyRepository, IHttpContextAccessor httpcontext)
        {
            _politcyRepository = politcyRepository;
            _httpContextAccessor = httpcontext;
             this.session = _httpContextAccessor.HttpContext?.Session!;
             this.userId = session.GetString("UserId")!;
        }


        public PolicyEnrollment GetPolicyByID(int id) {

            if (id > 0)
            {
                return _politcyRepository.FetchPolicyById(id);
            }
            else
            {
                throw new PolicyManagementException($"Id Should be Greated Than 0");
            }
           
        }

        public IEnumerable<PolicyEnrollment> GetAllPolicies()
        {

                return _politcyRepository.FetchAllPolicy();
        }

        public async Task<int> InsertPolicy(PolicyEnrollment policy)
        {
                  // For Enrolled on date
            DateTime currentDateTime = DateTime.Now;
				DateOnly formattedCurrentDate = new DateOnly(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day);

				policy.EnrolledOn = formattedCurrentDate;
                
                //commision calculation
                Plan plan = _politcyRepository.FetchPlanById(policy.PlanId);
                double commissionAmount = policy.CoverageAmount * (plan.CommissionPercentage / 100.0);
                policy.CommisionAmount = (long) commissionAmount ;

                // Expire Date
                int timeperriod = policy.TimePeriod;
                DateTime expireDate = DateTime.Now.AddYears(timeperriod);
                policy.ExpiredOn = new DateOnly(expireDate.Year,expireDate.Month, expireDate.Day);

                // Premium
                int frequency = cal.TimePeriod(policy.Frequency!);

                if (frequency > 0)
                {
                Client client;

                if(this.userId != null && this.userId != "9")
                {
                policy.ClientId = int.Parse(this.userId);
                   client = _politcyRepository.FetchClientById(policy.ClientId);
                }
                else
                {
                    policy.ClientId = 10;
                    client = _politcyRepository.FetchClientById(10);

                }


                  DateTime cliendDob = DateTime.ParseExact(client.Dob.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                  DateTime currentDate = DateTime.Today;

                  int age = currentDate.Year - cliendDob.Year;
                  Double actualCoverageFactor = cal.TotalTimePreriod(age);

                if (actualCoverageFactor >= policy.TimePeriod)
                {

                    long premium = policy.CoverageAmount / (policy.TimePeriod * frequency);
                    policy.Premium = premium;
                   /* Console.WriteLine($"Id: {policy.Id}");
                    Console.WriteLine($"PlanId: {policy.PlanId}");
                    Console.WriteLine($"AgentId: {policy.AgentId}");
                    Console.WriteLine($"ClientId: {policy.ClientId}");
                    Console.WriteLine($"CoverageAmount: {policy.CoverageAmount}");
                    Console.WriteLine($"Frequency: {policy.Frequency}");
                    Console.WriteLine($"Premium: {policy.Premium}");
                    Console.WriteLine($"CommisionAmount: {policy.CommisionAmount}");
                    Console.WriteLine($"EnrolledOn: {policy.EnrolledOn}");
                    Console.WriteLine($"CancelledOn: {policy.CancelledOn}");
                    Console.WriteLine($"ExpiredOn: {policy.ExpiredOn}");
                    Console.WriteLine($"TimePeriod: {policy.TimePeriod}");*/
                    return await _politcyRepository.InsertPolicy(policy);  

                }
                else
                {
                    throw new PolicyManagementException("Entered Time Period is not correct");

                }
               }
            else
            {
                throw new PolicyManagementException("Entered Frequency is not correct");
            }
        }

        public int UpdatePolicy(int id,PolicyEnrollment policy)
        {
           
            if (id > 0)
            {
                Console.WriteLine(policy.PlanId + " asdfsdf" +id);
                //commision calculation
                Plan plan = _politcyRepository.FetchPlanById(policy.PlanId);
                var commissionAmount = policy.CoverageAmount * (plan.CommissionPercentage / 100.0);
                policy.CommisionAmount = (long)commissionAmount ;

              
                int frequency = cal.TimePeriod(policy.Frequency!);

                if(frequency > 0) {
                 
                    Client client = _politcyRepository.FetchClientById(policy.ClientId);
                    Console.WriteLine(client.Id+ " Client id ");

                    DateTime cliendDob = DateTime.ParseExact(client.Dob.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    DateTime currentDate = DateTime.Today;

                    int age = currentDate.Year - cliendDob.Year;
                    Double actualCoverageFactor = cal.TotalTimePreriod(age);

                    if(actualCoverageFactor >= policy.TimePeriod) {

                        long premium = policy.CoverageAmount / (policy.TimePeriod * frequency);
                        policy.Premium = premium;
                        return _politcyRepository.UpdatePolicy(policy, id);
                    }
                    else
                    {
                        throw new PolicyManagementException("Entered Time Period is not correct");

                    }
                }
                else
                {
                    throw new PolicyManagementException("Entered Frequency is not correct");
                }  
            }
            else
            {
                throw new PolicyManagementException($"Id Should be Greated Than 0");
            }
        }

        public int DeletePolicy(PolicyEnrollment policy) {

            if (policy != null)
            {
                return _politcyRepository.DeletePolicy(policy);
            }
            else
            {
                throw new PolicyManagementException($"Id Should be Greated Than 0");
            }
        }

        public IEnumerable<City> GetAllCity()
        {
               return _politcyRepository.FetchAllCity();  
        }

        public IEnumerable<Plan> GetAllPlans()
        {
              return _politcyRepository.FetchAllPlan();

        }
        public IEnumerable<PolicyEnrollment> FilterPolicy(int id)
        {

            if (id > 0)
            {
                return _politcyRepository.FilterByCity(id);
            }
            else
            {
                throw new PolicyManagementException($"Id Should be Greated Than 0");
            }

        }

        public Plan GetPlanByID(int id,int userid)
        {
            if (id > 0)
            {
                Client client;
                if (userid != 9)
                {
                     client = _politcyRepository.FetchClientById(userid);

                }
                else
                {
                    client = _politcyRepository.FetchClientById(10);

                }
                Plan plan = _politcyRepository.FetchPlanById(id);
                //setting max coverage by age 

                DateTime cliendDob = DateTime.ParseExact(client.Dob.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime currentDate = DateTime.Today;

                int age = currentDate.Year - cliendDob.Year;

                Double actualCoverageFactor = cal.agePercentage(age);
                plan.MaxCoverageAmount =  (long) Math.Ceiling(plan.MaxCoverageAmount * actualCoverageFactor);

                return plan;
            }
            else
            {
                throw new PolicyManagementException($"Id Should be Greated Than 0");
            }
        }

        public IEnumerable<PolicyEnrollment> OrderBy()
        {
            return _politcyRepository.AscendingOrder();

        }

        public IEnumerable<Provider> GetAllProvider()
        {
            return _politcyRepository.FetchAllProvider();
        }

        public Client GetClientById( int id)
        {
            Console.WriteLine("fetching client " + id);
            if(id > 0)
            {
            return _politcyRepository.FetchClientById(id);

            }
            else
            {
                throw new PolicyManagementException($"User Id {id} Should be greater than 0");
            }
        }


        public int GetClientIdByUserName(string userName)
        {
            return _politcyRepository.FetchClientIdByUserName(userName);
        }
        public int GetPlanIdByPlanName(string planName, string providerName)
        {
            return _politcyRepository.FetchPlanIdByPlanName(planName, providerName);    
        }
        public int GetAgentIdByUserName(string userName)
        {
            return   _politcyRepository.FetchAgentIdByUserName (userName);
        }

    }
}
