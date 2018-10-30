[assembly: Microsoft.Owin.OwinStartup(typeof(SignalROwinApplication.Startup))]

namespace SignalROwinApplication
{
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Owin;
    
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.UseFileServer(new FileServerOptions()
            {
                FileSystem = new PhysicalFileSystem(GetRootDirectory()),
                EnableDirectoryBrowsing = true,
                RequestPath = new Microsoft.Owin.PathString("/html")
            });

            appBuilder.UseFileServer(new FileServerOptions()
            {
                FileSystem = new PhysicalFileSystem(GetScriptsDirectory()),
                EnableDirectoryBrowsing = true,
                RequestPath = new Microsoft.Owin.PathString("/scripts")
            });

            appBuilder.MapSignalR();

            var httpConfiguration = new HttpConfiguration();
            
            httpConfiguration.Formatters.Clear();
            httpConfiguration.Formatters.Add(new JsonMediaTypeFormatter());
            
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings = 
                new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            appBuilder.UseWebApi(httpConfiguration);
        }

        private static string GetRootDirectory()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var rootDirectory = Directory.GetParent(currentDirectory).Parent;
            Contract.Assume(rootDirectory != null);
            return Path.Combine(rootDirectory.FullName, "WebContent");
        }

        private static string GetScriptsDirectory()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var rootDirectory = Directory.GetParent(currentDirectory).Parent;
            Contract.Assume(rootDirectory != null);
            return Path.Combine(rootDirectory.FullName, "Scripts");
        }
    }
}
