using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HelpDeskNetSS.Startup))]
namespace HelpDeskNetSS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
