using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomAuthorization.Startup))]
namespace CustomAuthorization
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
