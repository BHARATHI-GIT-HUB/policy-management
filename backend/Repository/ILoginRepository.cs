using RepositryAssignement.Models;

namespace RepositryAssignement.Repository
{
    public interface ILoginRepository
    {
        public User Login(string? username, string? password);
        public User GetUserRole(string? userName);

    }
}
