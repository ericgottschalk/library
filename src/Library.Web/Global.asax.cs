using Library.Web.App_Start;
using Library.Web.Filters;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Library.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = true;
            GlobalFilters.Filters.Add(new GlobalExceptionFilter());
        }
    }
}
