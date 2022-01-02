using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;

namespace WordShopExcersice.ConsoleApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration CONFIG = new HttpConfiguration();
            
            CONFIG.Routes.MapHttpRoute(
                name: "createUserApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            appBuilder.UseWebApi(CONFIG);
        }
    }
}
