using System.Diagnostics;
using RepositryAssignement.Models;
using RepositryAssignement.Custome_Exception;
using Microsoft.EntityFrameworkCore;
namespace RepositryAssignement.Repository
{
    public class LoginRepository: ILoginRepository
    {
        private readonly InsuranceDbContext _context;

        public LoginRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public int Login(string? username, string? password)
        {
            User? user = _context.Users.Where(u => u.Username == username).FirstOrDefault();
            if(user != null)
            {
                if(password == user.Password)
                {
                    User? isUser = _context.Users.FirstOrDefault(U => U.Id == user.Id);
                    if(isUser != null)
                    {
                        return isUser.Id;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    throw new IncorrectPasswordException($"Password For the User Name {password} is Incorrect");
                }
            }
            else
            {
                throw new UserNotFoundException($"User With Name {username} is Not Registered");
            }
            
        }


        public IEnumerable<Role> FetchRole()
        {
            var roles = _context.Roles.ToList();
            if(roles.Count>0)
            {
                return roles;
            }
            else
            {
                throw new PolicyManagementException("Error in Fetching All Roles");
            }
            
        }

        public User? GetUserRole(string userName)
        {
            return _context.Users.Include(U => U.Role).Where(U => U.Username == userName).FirstOrDefault();
        }
    }
}
