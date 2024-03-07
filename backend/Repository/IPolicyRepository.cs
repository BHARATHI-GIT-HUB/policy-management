using RepositryAssignement.Models;

namespace RepositryAssignement.Repository
{
    public interface IPolicyRepository
    {
        Task<int> InsertPolicy(PolicyEnrollment policy);
        int DeletePolicy(PolicyEnrollment id);
        int UpdatePolicy(PolicyEnrollment policy ,int id);
        IEnumerable<PolicyEnrollment> FetchAllPolicy();
        PolicyEnrollment FetchPolicyById(int id);
        Client FetchClientById(int id);
        Plan FetchPlanById(int id);
        Provider FetchProviderById(int id);
        IEnumerable<PolicyEnrollment> FilterByCity(int id);
        IEnumerable<City> FetchAllCity();
        IEnumerable<PolicyEnrollment> AscendingOrder();

        IEnumerable<Plan> FetchAllPlan();
        IEnumerable<Provider> FetchAllProvider();
        int UpdateProvider(int id,Provider provider );   
        int DeleteProvider(int id);
        IEnumerable<Client> FetchAllClient();
        int DeleteClient(int id);
        int UpdateClient(int id ,Client client);   
        IEnumerable<Agent> FetchAllAgent();
        int UpdateAgent(int id ,Agent agent);   
        int DeleteAgent(int id);
        


        IEnumerable<dynamic> FetchAllOptimized();
        int FetchClientIdByUserName(string userName);
        int FetchPlanIdByPlanName(string planName,string providerName);
        int FetchAgentIdByUserName(string userName);
    }
}
