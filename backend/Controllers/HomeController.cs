using Microsoft.AspNetCore.Mvc;
using RepositryAssignement.BO;
using RepositryAssignement.Repository;

using RepositryAssignement.Custome_Exception;
using Microsoft.AspNetCore.Http;
using RepositryAssignement.Models;

namespace RepositryAssignement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LoginBO _loginBO = null;
        

        public HomeController(ILoginRepository loginRepository, ILogger<HomeController> logger)
        {
            _loginBO = new LoginBO(loginRepository);
            _logger = logger;
            _logger.LogInformation("Policy Management System Started..........");
        }
       

        [Route("/")]
        [Route("/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.Clear();

            return View();
        }


        [Route("/Login")]
        [HttpPost]
        public IActionResult Login(IFormCollection collection)
        {
            try
            {
               
                var username = collection["username"];
                var password = collection["password"];

                _logger.LogInformation($"Login Details {username} {password}");
                
                User user = _loginBO.Login(username, password);         
                
                if (user != null)
                {
                    _logger.LogInformation($"Valid Login Details");
                

                    HttpContext.Session.SetString("UserName", username);
                    HttpContext.Session.SetString("UserId", user.Id.ToString());

                    return Redirect("Policy/Index");
                }
                
            }
            catch(UserNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                _logger.LogError(ex.Message.ToString());
                Console.WriteLine(ex);
            }
            catch (IncorrectPasswordException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                _logger.LogError(ex.Message.ToString());
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                _logger.LogError($"Not A Valid Login Details {ex.Message}");
                Console.WriteLine(ex);
            }

            return View();
        }
    }
}
