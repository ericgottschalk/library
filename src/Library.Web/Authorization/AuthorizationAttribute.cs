using Library.Application.Handlers;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Library.Web.Authorization
{
    public sealed class AuthorizationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var singedMember = SessionControl.SingedMember;

            if (singedMember != null && AuthorizeCore(filterContext.HttpContext))
            {
                var myIdentity = new GenericIdentity(singedMember.Email);
                var principal = new GenericPrincipal(myIdentity, new string[1] { "Member" });

                Thread.CurrentPrincipal = principal;
                HttpContext.Current.User = principal;

                base.OnAuthorization(filterContext);
            }
            else
            {
                RedirectToLogin(filterContext);
            }
        }


        private void RedirectToLogin(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                                                new RouteValueDictionary
                                                {
                                                    { "action", "Login" },
                                                    { "controller", "Member" }
                                                });
        }
    }
}