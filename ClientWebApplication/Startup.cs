using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClientWebApplication.Startup))]
namespace ClientWebApplication
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
