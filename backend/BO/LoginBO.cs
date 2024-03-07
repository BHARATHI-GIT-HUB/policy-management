using Microsoft.CodeAnalysis.CSharp.Syntax;
using RepositryAssignement.Models;
using RepositryAssignement.Repository;

namespace RepositryAssignement.BO
{
    public class LoginBO
    {

        public readonly ILoginRepository _loginRepository;

        public LoginBO(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public User Login(string? username, string? password)
        {
            if (username == "") {
                throw new ArgumentNullException("username");
            }
            else if(password == "")
                { throw new ArgumentNullException("password"); }
            else{
             
                return _loginRepository.Login(username, password);

            }
        }

    }
}
