using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nc.Erp.WorksSupport.Api
{
    using System.Web;

    public class WebApiApplication : HttpApplication
    {
        public static string UserName { get; set; }

        public static string Password { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
