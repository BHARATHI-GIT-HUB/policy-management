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
        IEnumerable<Plan> FetchAllPlan();
        IEnumerable<City> FetchAllCity();
        IEnumerable<PolicyEnrollment> AscendingOrder();
        IEnumerable<Provider> FetchAllProvider();
        int FetchClientIdByUserName(string userName);
        int FetchPlanIdByPlanName(string planName,string providerName);
        int FetchAgentIdByUserName(string userName);
    }
}
