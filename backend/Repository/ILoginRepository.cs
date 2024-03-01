using RepositryAssignement.Models;

namespace RepositryAssignement.Repository
{
    public interface ILoginRepository
    {
        public int Login(string? username, string? password);
        public User GetUserRole(string? userName);

    }
}
