using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography;
using System.Transactions;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using RepositryAssignement.BO;
using RepositryAssignement.Custome_Exception;
using RepositryAssignement.Models;

namespace RepositryAssignement.Repository
{
    public class PolicyRepository : IPolicyRepository
    {

        private readonly InsuranceDbContext _context;

        public PolicyRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public Task<int> InsertPolicy(PolicyEnrollment policy)
        {
            var addedPolicy = _context.PolicyEnrollments.Add(policy);
            if (addedPolicy != null)
            {
                _context.SaveChanges();
                return Task.FromResult(policy.Id);
            }
            else
            {
                throw new InvalidOperationException();
            }

        }

        public PolicyEnrollment FetchPolicyById(int id)
        {
            /*    PolicyEnrollment? policy = _context.PolicyEnrollments
                    .Include(pole => pole.Agent).ThenInclude(Agn => Agn.User)
                    .Include(pole => pole.Client).ThenInclude(cli => cli.User)
                    .Include(pole => pole.Plan).ThenInclude(pln => pln.Provider).ThenInclude(pro => pro.User)
                    .FirstOrDefault(pole => pole.Id == id);*/


            PolicyEnrollment? policy = _context.PolicyEnrollments
                .Include(pole => pole.Agent).ThenInclude(Agn => Agn.User)
                .Include(pole => pole.Client).ThenInclude(cli => cli.User)
                .Include(pole => pole.Plan)
                .ThenInclude(pln => pln.Provider).ThenInclude(pro => pro.User)
                .FirstOrDefault(pole => pole.Id == id);

            if (policy != null)
            {
                return policy;
            }
            else
            {

                throw new PolicyNotFoundException($"Policy With Id {id} Not Found ");
            }

        }


        public IEnumerable<PolicyEnrollment> FetchAllPolicy()
        {

            IEnumerable<PolicyEnrollment> policies = _context.PolicyEnrollments
                .Include(pole => pole.Agent).ThenInclude(Agn => Agn.User)
                .Include(pole => pole.Client).ThenInclude(cli => cli.City)
                .Include(pole => pole.Plan)
                .ThenInclude(pln => pln.Provider)
                .ThenInclude(pro => pro.User)
                .ToList();



            if (policies.Any())
            {
                return policies;
            }
            else
            {
                throw new PolicyNotFoundException("Policies Not Found");
            }

        }


        public IEnumerable<dynamic> FetchAllOptimized()
        {

            var policies = _context.PolicyEnrollments
                .Include(pole => pole.Client)
                .Include(pole => pole.Plan)
                .Include(pole => pole.Agent)
               .Select(pole => new
               {
                   pole.Id,
                   pole.PlanId,
                   pole.AgentId,
                   pole.ClientId,
                   pole.CoverageAmount,
                   pole.Frequency,
                   pole.Premium,
                   pole.CommisionAmount,
                   pole.EnrolledOn,
                   pole.CancelledOn,
                   pole.ExpiredOn,
                   pole.TimePeriod,
                   Client = new { Id = pole.Client.Id, User = pole.Client.User },
                   Agent = new { Id = pole.Agent.Id, User = pole.Agent.User },
                   Plan = new { Id = pole.Plan.Id, PlanName = pole.Plan.PlanName, User = pole.Plan.Provider }

               })
                .ToList();

            if (policies.Any())
            {
                return policies;
            }
            else
            {
                throw new PolicyNotFoundException("Policies Not Found");
            }
        }
        public int UpdatePolicy(PolicyEnrollment policy, int id)
        {
            Console.WriteLine(id+ " id in update");
            var existingPolisy = FetchPolicyById(id);

            policy.Id = id;
            _context.Entry(existingPolisy).CurrentValues.SetValues(policy);
            _context.SaveChanges();
            return 0;

        }

        public int DeletePolicy(PolicyEnrollment id)
        {

            var response = _context.PolicyEnrollments.Remove(id);
            if (response != null)
            {
                _context.SaveChanges();
                return 0;
            }
            else
            {
                throw new PolicyManagementException($"Error In Deleteing the policy with id {id.Id}");
            }

        }

        public IEnumerable<City> FetchAllCity()
        {
            IEnumerable<City> cities = _context.Cities.ToList();
            if (cities.Any())
            {
                return cities;
            }
            else
            {
                throw new PolicyManagementException($"Error in Fetching all the City");
            }
        }

        public IEnumerable<PolicyEnrollment> FilterByCity(int id)
        {
            List<PolicyEnrollment> filteredPolicys = _context.PolicyEnrollments
                .Include(PE => PE.Client).ThenInclude(CLI => CLI.City)
                .Include(PE => PE.Plan)
                .Where(PE => PE.Client.City.Id == id).ToList();

            if (filteredPolicys.Any())
            {
                return filteredPolicys;
            }
            else
            {
                throw new PolicyManagementException($"Error in Filtering by the City with id {id}");
            }

        }

        public IEnumerable<Plan> FetchAllPlan()
        {
            List<Plan> plans = _context.Plans
                .Include(PL => PL.Provider)
                .Include(PL => PL.Subtype)
                .ThenInclude(Sub => Sub.Type)
                .ToList();

            if (plans.Any())
            {
                return plans;
            }
            else
            {
                throw new PolicyManagementException($"Error in Fetching all the plans");
            }

        }

        public Plan FetchPlanById(int id)
        {
            Plan? plan = _context.Plans
                .Include(PL => PL.Provider)
                .FirstOrDefault(Pl => Pl.Id == id);

            if (plan != null)
            {
                return plan;
            }
            else
            {
                throw new PolicyManagementException($"Error in Fetching the plan by id {id}");
            }

        }

        public IEnumerable<PolicyEnrollment> AscendingOrder()
        {

            IEnumerable<PolicyEnrollment> policies = _context.PolicyEnrollments.Include(pole => pole.Agent).ThenInclude(Agn => Agn.User)
                .Include(pole => pole.Client).ThenInclude(cli => cli.City)
                .Include(pole => pole.Plan).ThenInclude(pln => pln.Provider).ThenInclude(pro => pro.User)
                .OrderByDescending(pole => pole.CoverageAmount)
                .ToList();

            if (policies.Any())
            {
                return policies;
            }
            else
            {
                throw new PolicyManagementException($"Error in Ordering the policy");
            }

        }


        public IEnumerable<Provider> FetchAllProvider()
        {
            IEnumerable<Provider> providers = _context.Providers
                .Include("User").Include("City").ToList();
            if (providers.Any())
            {
                return providers;
            }
            else
            {
                throw new PolicyManagementException($"Error in Fetching all the providers");
            }
        }


        public Provider FetchProviderById(int id)
        {
            Provider? provider = _context.Providers.FirstOrDefault(pro => pro.Id == id);

            if (provider != null)
            {
                return provider;
            }
            else
            {
                throw new PolicyManagementException($"Error in Fetching the Provider by id {id}");
            }

        }


        public Client FetchClientById(int id)
        {
            Client? client = _context.Clients
                             .FirstOrDefault(Cli => Cli.Id == id);

            if (client != null)
            {
                return client;
            }
            else
            {
                throw new PolicyManagementException($"Error in Fetching Client by user Name by id {id}");
            }

        }


        public int FetchClientIdByUserName(string userName)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Client client = _context.Clients
                           .Include(Cli => Cli.User)
                           .FirstOrDefault(Cli => Cli.User != null && Cli.User.Username == userName);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (client != null)
            {
                return client.Id;
            }
            else
            {
                throw new PolicyManagementException($"Client with user name {userName} Not Fount");
            }
        }
        public int FetchPlanIdByPlanName(string planName, string providerName) {
            Plan? plan = _context.Plans
                       .Include(pln => pln.Provider)
                       .Where(pln => pln.Provider != null && pln.Provider.CompanyName == providerName)
                       .Where(pln => pln.PlanName == planName)
                       .FirstOrDefault();


            if (plan != null)
            {
                return plan.Id;
            }
            else
            {
                throw new PolicyManagementException($"Plan ${planName} with Provider {providerName} Not Fount");
            }
        }
        public int FetchAgentIdByUserName(string userName)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Agent agent = _context.Agents
                .Include(Agn => Agn.User)
                .FirstOrDefault(Agn => Agn.User != null && Agn.User.Username == userName);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (agent != null)
            {
                return agent.Id;
            }
            else
            {
                throw new PolicyManagementException($"Agent with user name {userName} Not Fount");
            }
        }

        public int UpdateProvider(int id, Provider provider) {
            {
                var existingProvider = _context.Providers.FirstOrDefault(Pro => Pro.Id == id);
                if (existingProvider != null) {
                    provider.Id = id;
                    _context.Entry(existingProvider).CurrentValues.SetValues(provider);
                    _context.SaveChanges();
                    return 0;
                }
                else
                {
                    throw new Exception($"Provider with id {id} not found");
                }

            }

           
        }
        public int DeleteProvider(int id)
        {
            Provider provider = _context.Providers.FirstOrDefault(Pro => Pro.Id == id);
            if (provider != null)
            {
                _context.Providers.Remove(provider);
                return 0;

            }
            else
            {
                throw new Exception($"Provider with id {id} not found ");
            }
        }

        public IEnumerable<Client> FetchAllClient()
        {
            return _context.Clients.Include(Cli => Cli.User).Include(Cli => Cli.City)
                .ToList();
        }

        public int DeleteClient(int id)
        {
            Client client = _context.Clients.FirstOrDefault(Cl => Cl.Id == id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges(true);
                return 0;
            }
            else
            {
                throw new Exception($"Client with id {id} not found ");
            }
        }

        public int UpdateClient(int id, Client client)
        {

            var existingClient = _context.Clients.FirstOrDefault(Cli => Cli.Id == id);
            if (existingClient != null)
            {
                client.Id = id;
                _context.Entry(existingClient).CurrentValues.SetValues(client);
                _context.SaveChanges();
                return 0;
            }
            else
            {
                throw new Exception($"Client with id {id} not found");
            }
        }

        public IEnumerable<Agent> FetchAllAgent()
        {
            return _context.Agents.Include(Agn => Agn.User).Include(Agn => Agn.City).ToList();
        }

        public int UpdateAgent(int id, Agent agent)
        {
            var existingAgent = _context.Agents.FirstOrDefault(Agn => Agn.Id == id);
            if (existingAgent != null)
            {
                agent.Id = id;
                _context.Entry(existingAgent).CurrentValues.SetValues(agent);
                _context.SaveChanges();
                return 0;
            }
            else
            {
                throw new Exception($"Agent with id {id} not found");
            }
        }

        public int DeleteAgent(int id)
        {
            Agent agent = _context.Agents.FirstOrDefault(Ag => Ag.Id == id);
            if (agent != null)
            {
                _context.Agents.Remove(agent);
                return 0;

            }
            else
            {
                throw new Exception($"Agent with id {id} not found ");
            }
        }
    } }
