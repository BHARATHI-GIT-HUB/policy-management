using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using RepositryAssignement.Models;
using Microsoft.EntityFrameworkCore;

namespace RepositryAssignement.Filter
{
    public class RoleBasedAuthAttribute : ActionFilterAttribute
    {
        private readonly string[] _allowedRoles;

        public RoleBasedAuthAttribute(params string[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userName = context.HttpContext.Session.GetString("UserName");
            if (userName == null)
            {
                context.Result = new RedirectResult("~/Login");
                return;
            }
            using (var contex = new InsuranceDbContext()) {
                User? user = contex.Users.Include(U => U.Role).Where(U => U.Username == userName).FirstOrDefault();

                bool isAuthorized = false;
                foreach (var allowedRole in _allowedRoles)
                {
                    if (user != null && user.Role.Type == allowedRole)
                    {
                        isAuthorized = true;
                        break;
                    }
                }

                if (!isAuthorized)
                {
                    // Redirect or return unauthorized result based on your requirement
                    context.Result = new RedirectResult("~/Login");
                    return;
                }
            }
            base.OnActionExecuting(context);


        }
    }
}
