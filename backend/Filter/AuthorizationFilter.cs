using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace RepositryAssignement.Filter
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        private readonly string[] _allowedRoles;

        public AuthorizationFilter(params string[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userName = filterContext.HttpContext.Session.GetString("UserName");
            if ( userName == null)
            {
                filterContext.Result = new RedirectResult("~/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }



    }
}
