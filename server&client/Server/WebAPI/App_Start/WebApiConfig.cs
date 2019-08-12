using AutoMapper;
using System.Web.Http;
using System.Web.Http.Cors;
using Common;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.
                Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);


            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //   name: "ActionApi",
            //   routeTemplate: "api/{controller}/{action}/{id}",
            //   defaults: new { id = RouteParameter.Optional }
            //);

            var cors = new EnableCorsAttribute("http://localhost:4200", "*", "*");
            config.EnableCors(cors);

            config.Routes.MapHttpRoute(
               name: "RouteApi",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional }
           );
        }
    }
}
