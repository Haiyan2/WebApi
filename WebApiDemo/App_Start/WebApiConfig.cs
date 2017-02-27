using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net.Http.Formatting;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using WebApiContrib.Formatting.Jsonp;


namespace WebApiDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //config.Formatters.Add(new CustomJsonFormater());

            // config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            // config.Formatters.Remove(config.Formatters.JsonFormatter);
            /*
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            */

            /*
            // Cross domain using jsonp
            var jsonFormatter = new JsonMediaTypeFormatter(config.Formatters.JsonFormatter);
            config.Formatters.Insert(0, jsonFormatter);
            */

            /*
            // Enable cross domain: orgins, headers, method using Cors
            EnableCorsAttribute cors = new EnableCorsAttribute("*","*","Get, Put");
            config.EnableCors(cors);
            */

            /*
            // Enable the https routing accross the entire web api controllers (within application) 
            // need to import the certificate then generate the pub key and import to the trust root
            config.Filters.Add(new RequireHttpsAttribute());
            */

            /*
            // enable authentication accross the entire web api controllers (within application)
            // Or use [BasicAuthentication] on the controller or action level
            config.Filters.Add(new BasicAuthenticationAttribute());
            */

            // TODo
            // web api solution is individual user accounts
        }

        /*
        public class CustomJsonFormater : JsonMediaTypeFormatter
        {
            public CustomJsonFormater()
            {
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            }

            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
            {
                base.SetDefaultContentHeaders(type, headers, mediaType);
                headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
        }
        */
    }
}
