using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BrightWorks.Startup))]
namespace BrightWorks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
