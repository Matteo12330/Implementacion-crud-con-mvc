using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BiteSpot.Helpers
{
    public class LoginAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var usuarioId = httpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                // Redirige correctamente a Account/Login
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
